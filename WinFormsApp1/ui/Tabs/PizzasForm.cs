using WinFormsApp1.forms;
using WinFormsApp1.repositories;

namespace WinFormsApp1;

public partial class PizzasForm : Tab<Pizza>
{
    private PizzaBaseRepository _baseRepo;
    private IngredientRepository _ingredientRepo;
    private PizzaRepository _repo;
    public PizzasForm(PizzaRepository repo, PizzaBaseRepository baseRepo, IngredientRepository ingredientRepo) : base(repo, "Пиццы")
    {
        _baseRepo = baseRepo;
        _ingredientRepo = ingredientRepo;
        _repo = repo;
    }
    
    protected override void EditButton_Click(object sender, EventArgs e)
    {
        var selected = listBox.SelectedItem as Pizza;
        if (selected == null) return;

        using var form = new EditPizzaForm(selected, _baseRepo, _ingredientRepo);
        if (form.ShowDialog() == DialogResult.OK)
        {
            var updated = form.GetResult();
            try
            {
                _repo.Update(selected.Name, updated.Text, updated.pizzaBase, updated.List);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadData();
        }
    }
    
    protected override void AddButton_Click(object sender, EventArgs e)
    {
        using var form = new AddPizzaForm(_baseRepo, _ingredientRepo);
        if (form.ShowDialog() == DialogResult.OK)
        {
            var newPizza = form.GetResult();
            try
            {
                _repo.Add(newPizza);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadData();
        }
    }
    
    protected override void DeleteButton_Click(object sender, EventArgs e)
    {
        var selected = listBox.SelectedItem as Pizza;
        if (selected == null) return;
        try
        {
            _repo.Delete(selected.Name);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        LoadData();
    }
}
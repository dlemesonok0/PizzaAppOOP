using WinFormsApp1.forms;
using WinFormsApp1.logic.repositories;
using WinFormsApp1.repositories;

namespace WinFormsApp1;

public partial class PizzasForm : Tab<Pizza>
{
    private PizzaBaseRepository _baseRepo;
    private IngredientRepository _ingredientRepo;
    private PizzaRepository _repo;
    private PizzaCrustRepository _pizzaCrustRepo;
    public PizzasForm(PizzaRepository repo, PizzaBaseRepository baseRepo, IngredientRepository ingredientRepo, PizzaCrustRepository pizzaCrustRepo) : base(repo, "Пиццы")
    {
        _baseRepo = baseRepo;
        _ingredientRepo = ingredientRepo;
        _pizzaCrustRepo = pizzaCrustRepo;
        _repo = repo;
    }
    
    protected override void EditButton_Click(object sender, EventArgs e)
    {
        var selected = listBox.SelectedItem as Pizza;
        if (selected == null) return;

        using var form = new EditPizzaForm(selected, _baseRepo, _ingredientRepo, _pizzaCrustRepo);
        if (form.ShowDialog() == DialogResult.OK)
        {
            var updated = form.GetResult();
            try
            {
                _repo.Update(selected.Name, updated.Text, updated.pizzaBase, updated.pizzaCrust, updated.List);
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
        using var form = new AddPizzaForm(_baseRepo, _ingredientRepo, _pizzaCrustRepo);
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
            _repo.Delete(selected.Id);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        LoadData();
    }
    
    protected override void FilterButton_Click(object sender, EventArgs e)
    {
        using var form = new PizzaFilterForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            Specification = form.GetSpecification();
            LoadData();
        }
    }
}
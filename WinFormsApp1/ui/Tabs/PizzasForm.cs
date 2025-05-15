using WinFormsApp1.forms;
using WinFormsApp1.repositories;

namespace WinFormsApp1;

public partial class PizzasForm : Tab<Pizza>
{
    private PizzaBaseRepository _baseRepo;
    private IngredientRepository _ingredientRepo;
    public PizzasForm(PizzaRepository repo, PizzaBaseRepository baseRepo, IngredientRepository ingredientRepo) : base(repo, "Пиццы")
    {
        _baseRepo = baseRepo;
        _ingredientRepo = ingredientRepo;
    }
    
    protected override void EditButton_Click(object sender, EventArgs e)
    {
        var selected = listBox.SelectedItem as Pizza;
        if (selected == null) return;

        using var form = new EditPizzaForm(selected, _baseRepo, _ingredientRepo);
        if (form.ShowDialog() == DialogResult.OK)
        {
            var updated = form.GetResult();
            _repo.Update(selected.Name, updated);
            LoadData();
        }
    }
    
    protected override void AddButton_Click(object sender, EventArgs e)
    {
        using var form = new AddIngredientForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            var newIngredient = form.GetResult();
            _repo.Add(newIngredient.Text, newIngredient.Value);
            LoadData();
        }
    }
}
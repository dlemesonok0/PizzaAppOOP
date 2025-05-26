using System.ComponentModel;
using WinFormsApp1.forms;
using WinFormsApp1.logic.repositories;
using WinFormsApp1.repositories;

namespace WinFormsApp1;

public partial class PizzaCrustForm : Tab<PizzaCrust>
{
    protected PizzaCrustRepository _repo; 
    private PizzaRepository _pizzaRepository;
    private IngredientRepository _ingredientRepository;
    public PizzaCrustForm(PizzaCrustRepository repo, PizzaRepository pizzaRepository, IngredientRepository ingredientRepository) : base(repo, "Бортики")
    {
        _repo = repo;
        _pizzaRepository = pizzaRepository;
        _ingredientRepository = ingredientRepository;
    }

    protected override void EditButton_Click(object sender, EventArgs e)
    {
        var selected = listBox.SelectedItem as PizzaCrust;
        if (selected == null) return;

        using var form = new EditPizzaCrustForm(selected, _pizzaRepository, _ingredientRepository);
        if (form.ShowDialog() == DialogResult.OK)
        {
            var updated = form.GetResult();
            try
            {
                _repo.Update(selected.Name, updated.Text, updated.Ingredients, updated.List, updated.Mode);
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
        using var form = new AddPizzaCrustForm(_pizzaRepository, _ingredientRepository);
        if (form.ShowDialog() == DialogResult.OK)
        {
            var newCrust = form.GetResult();
            try
            {
                _repo.Add(newCrust.Text, newCrust.Ingredients, newCrust.List, newCrust.Mode);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadData();
        }
    }
    
    protected override void FilterButton_Click(object sender, EventArgs e)
    {
        using var form = new CrustFilterForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            Specification = form.GetSpecification();
            LoadData();
        }
    }
}
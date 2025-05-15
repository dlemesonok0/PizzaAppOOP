using WinFormsApp1.forms;
using WinFormsApp1.repositories;

namespace WinFormsApp1;

public partial class IngredientsForm : Tab<Ingredient>
{
    public IngredientsForm(IngredientRepository repo) : base(repo, "Ингредиенты")
    {
    }
    
    protected override void EditButton_Click(object sender, EventArgs e)
    {
        var selected = listBox.SelectedItem as Ingredient;
        if (selected == null) return;

        using var form = new EditIngredientForm(selected);
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
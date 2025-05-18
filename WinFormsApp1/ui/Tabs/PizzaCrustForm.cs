using System.ComponentModel;
using WinFormsApp1.forms;
using WinFormsApp1.logic.repositories;
using WinFormsApp1.repositories;

namespace WinFormsApp1;

public partial class PizzaCrustForm : Tab<PizzaCrust>
{
    protected PizzaCrustRepository _repo; 
    public PizzaRepository _pizzaRepository;
    public PizzaCrustForm(PizzaCrustRepository repo, PizzaRepository pizzaRepository) : base(repo, "Бортики")
    {
        _repo = repo;
        _pizzaRepository = pizzaRepository;
    }

    protected override void EditButton_Click(object sender, EventArgs e)
    {
        var selected = listBox.SelectedItem as PizzaCrust;
        if (selected == null) return;

        using var form = new EditPizzaCrustForm(selected, _pizzaRepository);
        if (form.ShowDialog() == DialogResult.OK)
        {
            var updated = form.GetResult();
            try
            {
                _repo.Update(selected.Name, updated.Text, updated.Value, updated.List, updated.Mode);
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
        using var form = new AddPizzaCrustForm(_pizzaRepository);
        if (form.ShowDialog() == DialogResult.OK)
        {
            var newIngredient = form.GetResult();
            try
            {
                _repo.Add(newIngredient.Text, newIngredient.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadData();
        }
    }
}
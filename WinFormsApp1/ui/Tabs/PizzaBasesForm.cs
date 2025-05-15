using WinFormsApp1.forms;
using WinFormsApp1.repositories;

namespace WinFormsApp1;

public partial class PizzaBasesForm : Tab<PizzaBase>
{
    public PizzaBasesForm(PizzaBaseRepository repo) : base(repo, "Основы")
    {
    }
    
    protected override void EditButton_Click(object sender, EventArgs e)
    {
        var selected = listBox.SelectedItem as PizzaBase;
        if (selected == null) return;

        using var form = new EditPizzaBaseForm(selected);
        if (form.ShowDialog() == DialogResult.OK)
        {
            var updated = form.GetResult();
            _repo.Update(selected.Name, updated);
            LoadData();
        }
    }
    
    protected override void AddButton_Click(object sender, EventArgs e)
    {
        using var form = new AddPizzaBaseForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            var newPizzaBase = form.GetResult();
            _repo.Add(newPizzaBase.Text, newPizzaBase.Value);
            LoadData();
        }
    }
}
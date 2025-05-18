using WinFormsApp1.forms;
using WinFormsApp1.repositories;

namespace WinFormsApp1;

public partial class PizzaBasesForm : Tab<PizzaBase>
{
    private new PizzaBaseRepository _repo;
    public PizzaBasesForm(PizzaBaseRepository repo) : base(repo, "Основы")
    {
        _repo = repo;
    }
    
    protected override void EditButton_Click(object sender, EventArgs e)
    {
        var selected = listBox.SelectedItem as PizzaBase;
        if (selected == null) return;

        using var form = new EditPizzaBaseForm(selected);
        if (form.ShowDialog() == DialogResult.OK)
        {
            var updated = form.GetResult();
            try
            {
                _repo.Update(selected.Name, updated);
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
        using var form = new AddPizzaBaseForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            var newPizzaBase = form.GetResult();
            try
            {
                _repo.Add(new PizzaBase(newPizzaBase.Text, newPizzaBase.Value));
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
        var selected = listBox.SelectedItem as BaseEntity;
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
namespace WinFormsApp1.forms;

public partial class EditPizzaBaseForm : EditForm<PizzaBase>
{
    public EditPizzaBaseForm(PizzaBase pizzaBase) 
    {
        EntityName = pizzaBase.Name;
        EntityCost = pizzaBase.Cost;
        Entity = pizzaBase;
    }

    protected override void SaveEntity()
    {
        try
        {
            Entity = new PizzaBase(EntityName, EntityCost);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
namespace WinFormsApp1.forms;

public partial class EditPizzaBaseForm : EditForm<PizzaBase>
{
    public EditPizzaBaseForm(PizzaBase pizzaBase) 
    {
        EntityName = pizzaBase.Name;
        EntityCost = pizzaBase.Cost;
        Entity = pizzaBase;
    }
}
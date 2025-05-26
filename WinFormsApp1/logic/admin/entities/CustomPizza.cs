namespace WinFormsApp1.logic.admin.entities;

public class CustomPizza : Pizza
{
    public CustomPizza(string name, PizzaBase newPizzaBase, PizzaCrust pizzaCrust, IEnumerable<Ingredient> ingredients, Guid? id = null) : base(name, newPizzaBase, pizzaCrust, ingredients, id)
    {
    }
}
using WinFormsApp1.factories;
using WinFormsApp1.logic.repositories;

namespace WinFormsApp1.repositories;

public class PizzaRepository : Repository<Pizza>
{
    public PizzaRepository() : base(new PizzaFactory())
    {
    }

    public void Update(string oldName, string newName, PizzaBase pizzaBase, PizzaCrust pizzaCrust, List<Ingredient> ingredients)
    {
        var select = GetByName(oldName);
        if (select == null)
            throw new KeyNotFoundException("Pizza not found");
        if (GetByName(newName) != null && oldName != newName)
            throw new Exception("Pizza already exists");
        select.Update(newName, pizzaBase, pizzaCrust, ingredients);
    }
}
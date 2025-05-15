using WinFormsApp1.repositories;

namespace WinFormsApp1.factories;

public class PizzaFactory : Factory<Pizza>
{
    public override Pizza Create(string name, decimal cost)
    {
        throw new NotImplementedException("Для создания пиццы нужны ингредиенты и основа.");
    }

    public Pizza Create(string name, PizzaBase pizzaBase, IEnumerable<Ingredient> ingredients)
    {
        return new Pizza(name, pizzaBase, ingredients);
    }
}
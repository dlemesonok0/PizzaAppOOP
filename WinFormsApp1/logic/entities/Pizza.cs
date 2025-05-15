using WinFormsApp1.repositories;

namespace WinFormsApp1;

public class Pizza : BaseEntity
{
    public PizzaBase pizzaBase { get; private set; }
    public List<Ingredient> pizzaIngredients { get; protected set; }

    public override decimal Cost => pizzaBase.Cost + pizzaIngredients.Sum(i => i.Cost);
    public Pizza(string name, PizzaBase newPizzaBase, IEnumerable<Ingredient> ingredients) : base(name, 0)
    {
        pizzaBase = newPizzaBase;
        foreach (var ingredient in ingredients)
        {
            pizzaIngredients.Add(ingredient);
        }
        Console.WriteLine($"Added pizza {name}");
    }

    public void EditBase(PizzaBase newPizzaBase)
    {
        pizzaBase = newPizzaBase;
        Console.WriteLine($"Edited pizza {this} (base: {newPizzaBase})");
    }

    public void AddIngredient(Ingredient ingredient)
    {
        pizzaIngredients.Add(ingredient);
        Console.WriteLine($"Edited pizza {this} (new ingredient: {ingredient})");
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        pizzaIngredients.Remove(ingredient);
        Console.WriteLine($"Edited pizza {this} (remove ingredient: {ingredient})");
    }

    public override string ToString()
    {
        return $"{Name} - ({Cost}) : PizzaBase: {pizzaBase}, PizzaIngredients: {string.Join(" ", pizzaIngredients)}";
    }
}
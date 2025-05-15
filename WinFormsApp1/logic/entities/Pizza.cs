using WinFormsApp1.repositories;

namespace WinFormsApp1;

public class Pizza : BaseEntity
{
    public PizzaBase pizzaBase { get; protected set; }
    public Repository<Ingredient> pizzaIngredients { get; protected set; }

    public override decimal Cost => pizzaBase.Cost + pizzaIngredients.GetAll().Sum(i => i.Cost);
    public Pizza(string name, PizzaBase newPizzaBase, List<Ingredient> ingredients) : base(name, 0)
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
        pizzaIngredients.Delete(ingredient.Name);
        Console.WriteLine($"Edited pizza {this} (remove ingredient: {ingredient})");
    }

    public void EditIngredient(string oldName, string newName, decimal newCost)
    {
        var ingredient = pizzaIngredients.GetByName(oldName);
        pizzaIngredients.Delete(oldName);
        pizzaIngredients.Add(new Ingredient(newName, newCost));
        Console.WriteLine($"Edited pizza {this} (edited ingredient: {ingredient})");
    }

    public override string ToString()
    {
        return $"{Name} - ({Cost}) : PizzaBase: {pizzaBase}, PizzaIngredients: {string.Join(" ", pizzaIngredients.GetAll())}";
    }
}
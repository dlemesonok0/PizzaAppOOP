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
        pizzaIngredients = [];
        foreach (var ingredient in ingredients)
        {
            pizzaIngredients.Add(ingredient);
        }
        Console.WriteLine($"Added pizza {name}");
    }

    public new void Update(string newName, PizzaBase pizzaBase, List<Ingredient> ingredients)
    {
        EditName(newName);
        EditBase(pizzaBase);
        pizzaIngredients = ingredients;
    }

    public void EditName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) 
            throw new ArgumentNullException(nameof(name), " cannot be null or empty.");
        Name = name;
    }

    public void EditBase(PizzaBase newPizzaBase)
    {
        pizzaBase = newPizzaBase;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        pizzaIngredients.Add(ingredient);
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        pizzaIngredients.Remove(ingredient);
    }

    public override string ToString()
    {
        return $"{Name} - ({Cost}) : PizzaBase: {pizzaBase}, PizzaIngredients: {string.Join(" ", pizzaIngredients)}";
    }
}
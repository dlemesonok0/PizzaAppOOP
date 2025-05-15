namespace WinFormsApp1;

public class Pizza : Product
{
    public Base PizzaBase { get; protected set; }
    public IngredientManager PizzaIngredients { get; protected set; }
    
    public Pizza(string name, Base newBase, List<Ingredient> ingredients)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }
        Name = name;
        PizzaBase = newBase;
        foreach (var ingredient in ingredients)
        {
            PizzaIngredients.AddElem(ingredient.Name, ingredient.Cost);
        }
        Cost = GetPrice();
        Console.WriteLine($"Added pizza {name}");
    }

    public void EditBase(Base newBase)
    {
        PizzaBase = newBase;
        Console.WriteLine($"Edited pizza {this} (base: {newBase})");
        Cost = GetPrice();
    }

    public void AddIngredient(Ingredient ingredient)
    {
        PizzaIngredients.AddElem(ingredient.Name, ingredient.Cost);
        Console.WriteLine($"Edited pizza {this} (new ingredient: {ingredient})");
        Cost = GetPrice();
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        PizzaIngredients.RemoveElem(ingredient.Name);
        Console.WriteLine($"Edited pizza {this} (remove ingredient: {ingredient})");
        Cost = GetPrice();
    }

    public void EditIngredient(string oldName, string newName, decimal newCost)
    {
        var ingredient = PizzaIngredients.FindElem(oldName);
        if (ingredient == null)
        {
            Console.WriteLine($"This ingredient isn't in pizza {this}");
            return;
        }
        PizzaIngredients.RemoveElem(oldName);
        PizzaIngredients.AddElem(newName, newCost);
        Cost = GetPrice();
        Console.WriteLine($"Edited pizza {this} (edited ingredient: {ingredient})");
    }
    protected decimal GetPrice()
    {
        decimal totalPrice = 0;
        totalPrice += PizzaBase.Cost;
        foreach (var ingredient in PizzaIngredients.Elems)
        {
            totalPrice += ingredient.Cost;
        }
        return totalPrice;
    }

    public override string ToString()
    {
        return $"{Name} - ({Cost}) : PizzaBase: {PizzaBase}, PizzaIngredients: {string.Join(" ", PizzaIngredients.Elems)}";
    }
}

public class PizzaManager : Manager
{
}
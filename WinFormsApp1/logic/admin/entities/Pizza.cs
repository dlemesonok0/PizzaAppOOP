using WinFormsApp1.repositories;

namespace WinFormsApp1;

public class Pizza : BaseEntity
{
    public PizzaBase Base { get; protected set; }
    public virtual List<Ingredient> PizzaIngredients { get; protected set; }
    
    public PizzaCrust? Crust { get; protected set; }

    public override decimal Cost 
    {
        get
        {
            decimal sum = Base.Cost + PizzaIngredients.Sum(i => i.Cost);
            if (Crust != null) sum += Crust.Cost;
            return sum;
        }
    }
    public Pizza(string name, PizzaBase newPizzaBase, PizzaCrust pizzaCrust, IEnumerable<Ingredient> ingredients) : base(name, 0)
    {
        Base = newPizzaBase;
        PizzaIngredients = [];
        Crust = pizzaCrust;
        if (ingredients != null)
        foreach (var ingredient in ingredients)
        {
            PizzaIngredients.Add(ingredient);
        }
    }

    public new void Update(string newName, PizzaBase pizzaBase, PizzaCrust pizzaCrust, List<Ingredient> ingredients)
    {
        if (pizzaCrust != null && pizzaCrust.IsCompatibleWith(this))
            throw new Exception("this crust doesn't fit to this pizza");
        Crust = pizzaCrust;
        EditName(newName);
        EditBase(pizzaBase);
        PizzaIngredients = ingredients;
    }

    public void EditName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) 
            throw new ArgumentNullException(nameof(name), " cannot be null or empty.");
        Name = name;
    }

    public void EditBase(PizzaBase newPizzaBase)
    {
        Base = newPizzaBase;
    }

    public void AddIngredient(Ingredient ingredient)
    {
        PizzaIngredients.Add(ingredient);
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        PizzaIngredients.Remove(ingredient);
    }

    public void EditCrust(PizzaCrust newCrust)
    {
        if (newCrust != null && newCrust.IsCompatibleWith(this))
            throw new Exception("this crust doesn't fit to this pizza");
        Crust = newCrust;
    }

    public override string ToString()
    {
        return $"{Name} - ({Cost:C}) : PizzaCrust: {Crust}, PizzaBase: {Base}, PizzaIngredients: {string.Join(" ", PizzaIngredients)}";
    }

    public override Pizza Clone()
    {
        List<Ingredient> ingredients = new List<Ingredient>();
        foreach (var ingredient in PizzaIngredients)
        {
            ingredients.Add(ingredient.Clone() as Ingredient);
        }
        return new Pizza(Name, Base.Clone() as PizzaBase, Crust, ingredients);
    }
}
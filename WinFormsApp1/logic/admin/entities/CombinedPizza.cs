namespace WinFormsApp1.logic.user;

public class CombinedPizza : Pizza
{
    public List<Pizza> Parts { get; private set; }

    public CombinedPizza(IEnumerable<Pizza> parts, PizzaBase Base, PizzaCrust crust) : base(string.Join("/", parts.Select(i => i.Name)), Base,
        (crust is PizzaCrust) ? crust : null, null)
    {
        Parts = new List<Pizza>(parts);

        PizzaIngredients = new List<Ingredient>(
            Parts.SelectMany(p => p.PizzaIngredients).ToList()
        );
    }

    public override decimal Cost => 
        Parts.Sum(p => p.Cost * 1 / Parts.Count);
    
    public new void Update(string newName, List<Pizza> pizzas, PizzaBase pizzaBase, PizzaCrust crust)
    {
        Name = newName;
        Base = pizzaBase;
        Crust = crust;
        Parts.Clear();
        Parts = pizzas;
        PizzaIngredients = new List<Ingredient>(
            Parts.SelectMany(p => p.PizzaIngredients).ToList()
        );
    }
}
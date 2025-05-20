namespace WinFormsApp1.logic.user;

public class CombinedPizza : Pizza
{
    public List<Pizza> Parts { get; }
    
    public override List<Ingredient> PizzaIngredients => 
        (List<Ingredient>) Parts.SelectMany<Pizza, Ingredient>(p => p.PizzaIngredients);

    public decimal Cost => 
        Parts.Sum(p => p.Cost * 1 / Parts.Count);

    public CombinedPizza(IEnumerable<Pizza> parts, PizzaBase Base) : base(string.Join("/", parts.Select(i => i.Name)), Base, null, null)
    {
        Parts = new List<Pizza>(parts);
    }
}
namespace WinFormsApp1;

public class PizzaCrust : BaseEntity
{
    public List<Ingredient> Ingredients { get; set; }
    public bool UseWhiteList = true;
    public List<Pizza> Compatibility { get; private set; } = new();
    
    public override decimal Cost => Ingredients.Sum(i => i.Cost);

    public PizzaCrust(string name, List<Ingredient> ingredients, List<Pizza> list, bool mode) : base(name, 0)
    {
        if (list == null) 
            throw new ArgumentException("list is null");
        if (ingredients == null)
            throw new ArgumentException("Ingredients is null");
        Ingredients = ingredients;
        Compatibility = list;
        UseWhiteList = mode;
    }

    public void Update(string newName, List<Ingredient> ingredients, List<Pizza> list, bool mode)
    {
        base.Update(newName, 0);
        if (list == null) 
            throw new ArgumentException("list is null");
        if (ingredients == null)
            throw new ArgumentException("Ingredients is null");
        Ingredients = ingredients;
        Compatibility = list;
        UseWhiteList = mode;
    }

    public bool IsCompatibleWith(Pizza pizza)
    {
        if (UseWhiteList) return !Compatibility.Contains(pizza);
        return Compatibility.Contains(pizza);
    }

    public override string ToString()
    {
        string mode = UseWhiteList ? "whiteList" : "blackList";
        return $"{Name} - ({Cost:C}) : CompabilityMode: {mode}, IngredientList: {string.Join(" ", Ingredients)}";
    }
}
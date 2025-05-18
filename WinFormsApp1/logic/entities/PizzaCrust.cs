namespace WinFormsApp1;

public class PizzaCrust : BaseEntity
{
    public bool UseWhiteList = true;
    public List<Pizza> Compatibility { get; private set; } = new();

    public PizzaCrust(string name, decimal cost, List<Pizza> list, bool mode) : base(name, cost)
    {
        Compatibility = list;
        UseWhiteList = mode;
    }

    public void Update(string newName, decimal newCost, List<Pizza> list, bool mode)
    {
        base.Update(newName, newCost);
        if (list == null) 
            throw new ArgumentException("list is null");
        Compatibility = list;
        UseWhiteList = mode;
    }

    public bool IsCompatibleWith(Pizza pizza)
    {
        if (UseWhiteList) return Compatibility.Contains(pizza);
        return !Compatibility.Contains(pizza);
    }

    public override string ToString()
    {
        string mode = UseWhiteList ? "whiteList" : "blackList";
        return $"{Name} - ({Cost:C}) : CompabilityMode: {mode}, PizzaList: {string.Join(" ", Compatibility)}";
    }
}
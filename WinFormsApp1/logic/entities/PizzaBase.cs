namespace WinFormsApp1;

public class PizzaBase : BaseEntity
{
    public PizzaBase(string name, decimal cost) : base(name, cost) {}

    public override void Update(string newName, decimal newCost)
    {
        if (Name == "Classic" && newName != "Classic")
            throw new ArgumentException("You cannot change the classic base name.");
        base.Update(newName, newCost);
    }
}
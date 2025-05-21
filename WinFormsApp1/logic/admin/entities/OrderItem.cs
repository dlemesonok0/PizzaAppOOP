namespace WinFormsApp1.logic.user;

public class OrderItem : BaseEntity
{
    public Pizza Pizza { get; set; }
    public SizePizza SizePizza { get; set; }
    public bool DuplicateIngredients { get; set; }

    public OrderItem(Pizza pizza, PizzaCrust pizzaCrust, SizePizza sizePizza, bool duplicateIngredients) : base("order", 0)
    {
        Pizza = pizza.Clone();
        if (pizzaCrust != null)
            Pizza.EditCrust(pizzaCrust.Clone() as PizzaCrust);
        SizePizza = sizePizza;
        DuplicateIngredients = duplicateIngredients;
    }

    public decimal Cost
    {
        get
        {
            var mul = (DuplicateIngredients ? 2 : 1);
            return (Pizza.Cost * mul - Pizza.Base.Cost * (mul - 1) -
                    ((Pizza.Crust != null) ? Pizza.Crust.Cost * (mul - 1) : 0)) * SizePizza.Multiplier();
        }
    }

    public override string ToString()
    {
        return $"Pizza: {Pizza.Name}, PizzaCrust: {Pizza.Crust} Size: {SizePizza}, Cost: {Cost:C}";
    }

    public override BaseEntity Clone()
    {
        throw new NotImplementedException();
    }
}
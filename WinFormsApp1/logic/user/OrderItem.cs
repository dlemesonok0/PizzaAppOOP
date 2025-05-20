namespace WinFormsApp1.logic.user;

public class OrderItem
{
    public Pizza Pizza { get; set; }
    public SizePizza SizePizza { get; set; }
    public bool DuplicateIngredients { get; set; }

    public OrderItem(Pizza pizza, SizePizza sizePizza, bool duplicateIngredients)
    {
        Pizza = pizza.Clone() as Pizza;
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
        return $"Pizza: {Pizza.Name}, Size: {SizePizza}, Cost: {Cost:C}";
    }
}
namespace WinFormsApp1.logic.user;

public class OrderItem
{
    public Pizza Pizza { get; set; }
    public SizePizza SizePizza { get; set; }
    public bool DuplicateIngredients { get; set; }

    public decimal Cost
    {
        get
        {
            var mul = (DuplicateIngredients ? 2 : 1);
            return (Pizza.Cost * mul - Pizza.Base.Cost * (mul - 1) - Pizza.Crust.Cost * (mul - 1)) * SizePizza.Multiplier();
        }
    }
}
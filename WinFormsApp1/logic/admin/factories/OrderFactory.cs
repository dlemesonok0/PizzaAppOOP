using WinFormsApp1.factories;

namespace WinFormsApp1.logic.user;

public class OrderFactory : Factory<Order>
{
    public override Order Create(string name, decimal cost)
    {
        return new Order(name);
    }
}
using WinFormsApp1.factories;
using WinFormsApp1.logic.repositories;

namespace WinFormsApp1.logic.user;

public class OrderRepository : Repository<Order>
{
    public OrderRepository() : base(new OrderFactory())
    {
    }
    
}
namespace WinFormsApp1.logic.user;

public class OrderService
{
    private readonly OrderRepository _repo;

    public OrderService(OrderRepository repository)
    {
        _repo = repository;
    }

    public Order CreateOrder(string name, List<OrderItem> items, string comment = "", DateTime? scheduledTime = null)
    {
        var order = new Order(name)
        {
            Comment = comment,
            ScheduledTime = scheduledTime
        };

        foreach (var item in items)
        {
            order.Items.Add(item);
        }
        
        _repo.Add(order);
        return order;
    }
    
    public void UpdateOrder(Order order)
    {
        _repo.Update(order.Name, order);
    }

    public void AddOrder(Order order)
    {
        _repo.Add(order);
    }
}
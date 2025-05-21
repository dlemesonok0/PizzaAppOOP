using WinFormsApp1.logic.admin.specification;
using WinFormsApp1.logic.user;

namespace WinFormsApp1.logic.admin.repositories;

public class OrderOnDateSpecification : ISpecification<Order>
{
    private readonly DateTime _date;

    public OrderOnDateSpecification(DateTime date) => 
        _date = date.Date;

    public bool IsSatisfiedBy(Order order) => 
        (order.ScheduledTime != null) && order.ScheduledTime.Value.Date == _date;
}
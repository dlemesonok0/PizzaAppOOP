using WinFormsApp1.logic.admin.specification;
using WinFormsApp1.logic.user;

namespace WinFormsApp1.logic.admin.repositories;

public class OrderCostLessThanSpecification : ISpecification<Order>
{
    private readonly decimal _maxCost;

    public OrderCostLessThanSpecification(decimal maxCost) => 
        _maxCost = maxCost;

    public bool IsSatisfiedBy(Order order) => 
        order.Cost < _maxCost;
}
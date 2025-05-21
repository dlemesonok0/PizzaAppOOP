using WinFormsApp1.logic.admin.specification;
using WinFormsApp1.logic.user;

namespace WinFormsApp1.logic.admin.repositories;

public class OrderContainsIngredientSpecification : ISpecification<Order>
{
    private readonly string _ingredientName;

    public OrderContainsIngredientSpecification(string ingredientName) => 
        _ingredientName = ingredientName;

    public bool IsSatisfiedBy(Order order)
    {
        return order.Items.Any(item =>
            item.Pizza.PizzaIngredients.Any(i => i.Name.Contains(_ingredientName, StringComparison.OrdinalIgnoreCase))
        );
    }
}
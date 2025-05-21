namespace WinFormsApp1.logic.admin.specification;

public class PizzaHasIngredientSpecification : ISpecification<Pizza>
{
    private readonly string _ingredientName;

    public PizzaHasIngredientSpecification(string ingredientName)
    {
        _ingredientName = ingredientName;
    }

    public bool IsSatisfiedBy(Pizza pizza)
    {
        return pizza.PizzaIngredients.Any(i => i.Name == _ingredientName);
    }
}
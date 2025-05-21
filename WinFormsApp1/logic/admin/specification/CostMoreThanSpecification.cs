namespace WinFormsApp1.logic.admin.specification;

public class CostMoreThanSpecification<T> : ISpecification<T> where T : BaseEntity 
{
    private readonly decimal _minCost;

    public CostMoreThanSpecification(decimal maxCost) => 
        _minCost = maxCost;

    public bool IsSatisfiedBy(T item) => 
        item.Cost > _minCost;
}
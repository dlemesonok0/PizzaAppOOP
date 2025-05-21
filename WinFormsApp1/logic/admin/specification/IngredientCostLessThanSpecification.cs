namespace WinFormsApp1.logic.admin.specification;

public class CostLessThanSpecification<T> : ISpecification<T> where T : BaseEntity
{
    private readonly decimal _maxCost;

    public CostLessThanSpecification(decimal maxCost) => 
        _maxCost = maxCost;

    public bool IsSatisfiedBy(T item) => 
        item.Cost < _maxCost;
}
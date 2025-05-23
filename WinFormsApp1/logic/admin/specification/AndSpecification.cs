namespace WinFormsApp1.logic.admin.specification;

public class AndSpecification<T> : ISpecification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public AndSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _left = left;
        _right = right;
    }

    public bool IsSatisfiedBy(T item) =>
        _left.IsSatisfiedBy(item) && _right.IsSatisfiedBy(item);
}
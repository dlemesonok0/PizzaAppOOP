namespace WinFormsApp1.logic.admin.specification;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T item);
}
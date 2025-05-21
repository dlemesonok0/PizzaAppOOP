namespace WinFormsApp1.logic.admin.specification;

public class NameContainsSpecification<T> : ISpecification<T> where T : BaseEntity
{
    private readonly string _text;

    public NameContainsSpecification(string text) => 
        _text = text;

    public bool IsSatisfiedBy(T item) => 
        item.Name.Contains(_text, StringComparison.OrdinalIgnoreCase);
}
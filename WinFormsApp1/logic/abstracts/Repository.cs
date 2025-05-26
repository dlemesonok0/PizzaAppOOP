using WinFormsApp1.factories;
using WinFormsApp1.logic.admin.specification;
using WinFormsApp1.repositories;

namespace WinFormsApp1.logic.repositories;

public abstract class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly List<T> _items = new();
    protected IFactory<T> _factory;
    
    protected Repository(IFactory<T> factory)
    {
        _factory = factory;
    }
    
    public void Add(string name, decimal cost)
    {
        _items.Add(_factory.Create(name, cost));
    }
    
    public void Add(BaseEntity entity)
    {
        var item = GetById(entity.Id);
        if (item != null) 
            throw new KeyNotFoundException($"{entity} is present in the repository");
        _items.Add(entity as T);
    }

    public virtual void Update(Guid id, string newName, decimal cost)
    {
        var item = GetById(id);
        if (item == null) 
            throw new KeyNotFoundException($"{id} is not present in the repository");
        item.Update(newName, cost);
    }
    
    public virtual void Update(Guid id, BaseEntity entity)
    {
        var item = GetById(id);
        if (item == null) 
            throw new KeyNotFoundException($"{id} is not present in the repository");
        item.Update(entity.Name, entity.Cost);
    }

    public virtual void Delete(Guid id) 
    {
        _items.Remove(GetById(id));
    }

    public T? GetById(Guid id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id, null);
        return item;
    }

    public T? GetByName(string name)
    {
        var item = _items.FirstOrDefault(x => x.Name == name, null);
        return item;
    }

    public IEnumerable<T> GetAll()
    {
        return _items;
    }
    
    public List<T> Find(ISpecification<T> specification) =>
        _items.Where(i => specification.IsSatisfiedBy(i)).ToList();
}
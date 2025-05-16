using System.ComponentModel;
using WinFormsApp1.factories;
using WinFormsApp1.util;

namespace WinFormsApp1.repositories;

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
        var item = GetByName(name);
        if (item != null) 
            throw new KeyNotFoundException($"{item} is present in the repository");
        _items.Add(_factory.Create(name, cost));
    }
    
    public void Add(BaseEntity entity)
    {
        var item = GetByName(entity.Name);
        if (item != null) 
            throw new KeyNotFoundException($"{entity} is present in the repository");
        _items.Add(entity as T);
    }

    public virtual void Update(string name, string newName, decimal cost)
    {
        var item = GetByName(name);
        if (item == null) 
            throw new KeyNotFoundException($"{name} is not present in the repository");
        item = GetByName(newName);
        if (item != null  && name != newName)
            throw new KeyNotFoundException($"{name} is present in the repository");
        item.Update(newName, cost);
    }
    
    public virtual void Update(string name, BaseEntity entity)
    {
        var item = GetByName(name);
        if (item == null) 
            throw new KeyNotFoundException($"{name} is not present in the repository");
        var exist = GetByName(entity.Name);
        if (exist != null && name != entity.Name)
            throw new KeyNotFoundException($"{name} is present in the repository");
        item.Update(entity.Name, entity.Cost);
    }

    public virtual void Delete(string name)
    {
        _items.Remove(GetByName(name));
    }

    public T GetByName(string name)
    {
        var item = _items.FirstOrDefault(x => x.Name == name);
        return item;
    }

    public IEnumerable<T> GetAll()
    {
        return _items;
    }
}
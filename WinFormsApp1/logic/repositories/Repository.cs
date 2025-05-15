using System.ComponentModel;
using WinFormsApp1.factories;
using WinFormsApp1.util;

namespace WinFormsApp1.repositories;

public abstract class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly BindableDictionary<string, T> _items = new();
    private Factory<T> _factory;

    public void Add(string name, decimal cost)
    {
        _items.Add(name, _factory.Create(name, cost));
    }
    
    public void Add(BaseEntity entity)
    {
        _items.Add(entity.Name, entity as T);
    }

    public void Update(string name, string newName, decimal cost)
    {
        if (!_items.ContainsKey(name))
            throw new KeyNotFoundException($"{name} is not present in the repository");
        
        _items.Remove(name);
        _items.Add(name, _factory.Create(name, cost));
    }

    public void Delete(string name)
    {
        _items.Remove(name);
    }

    public T GetByName(string name)
    {
        if (!_items.ContainsKey(name)) 
            throw new KeyNotFoundException($"{name} is not present in the repository");
        return _items[name];
    }

    public IEnumerable<T> GetAll()
    {
        return _items.Values;
    }
}
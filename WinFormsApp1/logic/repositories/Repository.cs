using System.ComponentModel;
using WinFormsApp1.util;

namespace WinFormsApp1.repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly BindableDictionary<string, T> _items = new();

    public void Add(T item)
    {
        _items.Add(item.Name, item);
    }

    public void Update(string name, T item)
    {
        if (!_items.ContainsKey(name))
            throw new KeyNotFoundException($"{name} is not present in the repository");
        
        _items.Remove(name);
        _items.Add(item.Name, item);
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
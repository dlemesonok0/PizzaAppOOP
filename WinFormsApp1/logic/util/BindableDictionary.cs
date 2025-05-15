using System.ComponentModel;

namespace WinFormsApp1.util;

public class BindableDictionary<TKey, TValue> : BindingList<KeyValuePair<TKey, TValue>>
{
    private readonly Dictionary<TKey, TValue> _dictionary = new();

    public BindableDictionary() { }

    public BindableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
    {
        foreach (var item in collection)
        {
            _dictionary[item.Key] = item.Value;
            base.Add(item);
        }
    }

    public void Add(TKey key, TValue value)
    {
        if (_dictionary.ContainsKey(key))
            throw new ArgumentException("Key is already present in the repository");

        _dictionary[key] = value;
        base.Add(new KeyValuePair<TKey, TValue>(key, value));
    }

    public bool Remove(TKey key)
    {
        if (_dictionary.TryGetValue(key, out var value))
        {
            _dictionary.Remove(key);
            return base.Remove(new(key, value));
        }
        return false;
    }

    public TValue this[TKey key]
    {
        get => _dictionary[key];
        set
        {
            if (_dictionary.ContainsKey(key))
            {
                _dictionary[key] = value;
                var index = Items.IndexOf(Items.FirstOrDefault(i => i.Key.Equals(key)));
                if (index >= 0)
                {
                    Items[index] = new KeyValuePair<TKey, TValue>(key, value);
                    OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
                }
            }
            else
            {
                Add(key, value);
            }
        }
    }
    
    public ICollection<TValue> Values
    {
        get { return this.Select(kvp => kvp.Value).ToList(); }
    }

    public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);
}
using System.ComponentModel;

namespace WinFormsApp1;

public abstract class Manager
{
    public readonly BindingList<Product> Elems = [];
    
    public virtual void AddElem(string name, decimal cost)
    {
        var temp = FindElem(name);
        
        var elem = CreateProduct(name, cost);
        if (temp == null)
        {
            Elems.Add(elem);
            Console.WriteLine($"Added {elem}");
        }
        else
        {
            Console.WriteLine($"{elem} is already in the list");
        }
    }

    public virtual void EditElem(string oldName, string newName, decimal newCost)
    {
        var elem = FindElem(oldName);
        if (elem == null)
        {
            throw new KeyNotFoundException($"Element {oldName} was not found.");
        }

        if (FindElem(newName) != null)
        {
            throw new ArgumentException($"The name {newName} already exists.");
        }
        elem.Update(newName, newCost);
        Elems[(int) FindIdxElem(newName)!] = elem;
        Console.WriteLine($"Updated {oldName} to {newName} with {newCost}");
    }

    public void RemoveElem(string name)
    {
        var elem = FindElem(name);
        if (elem == null)
        {
            Console.WriteLine($"Could not find ingredient {name}.");
            return;
        }
        Elems.Remove(elem);
        Console.WriteLine($"Removed {elem}");
    }

    public void ShowAllElems()
    {
        if (Elems.Count == 0)
        {
            Console.WriteLine("There are no ingredients.");
            return;
        }
        Console.WriteLine("Elements:");
        foreach (var elem in Elems)
        {
            Console.WriteLine($" - {elem}");
        }
    }
    
    public Product? FindElem(string name)
    {
        var elem = Elems.FirstOrDefault(i => i?.Name == name, null);
        return elem;
    }
    
    protected int? FindIdxElem(string name)
    {
        int? idx = null;
        for (var i = 0; i < Elems.Count; i++)
        {
            if (Elems[i].Name == name)
            {
                idx = i;
                break;
            }
        }
        return idx;
    }

    protected virtual Product CreateProduct(string name, decimal cost)
    {
        return new Product(name, cost);
    }
}
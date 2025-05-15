namespace WinFormsApp1;

public class Base : Product
{
    public Base(string name, decimal cost) : base(name, cost) {}
}

public class BaseManager : Manager
{
    protected override Product CreateProduct(string name, decimal cost)
    {
        return new Base(name, cost);
    }
    public override void AddElem(string baseName, decimal baseCost)
    {
        var classic = FindElem("классическое");
        if (classic == null)
        {
            if (baseName == "классическое")
            {
                var newClassic = CreateProduct(baseName, baseCost);
                Elems.Add(newClassic);
                Console.WriteLine($"Added {newClassic}");
                return;
            }
            Console.WriteLine($"Please, add 'классическое' base");
            return;
        }
        var elem = FindElem(baseName);
        if (elem == null)
        {
            var newBase = CreateProduct(baseName, baseCost);
            if (baseCost > (decimal)0.2 * classic.Cost)
            {
                Console.WriteLine($"{newBase} is more than 20 percent of {classic.Cost} (классическое cost)");
                return;
            }
            Elems.Add(newBase);
            Console.WriteLine($"Added {newBase}");
            return;
        }
        Console.WriteLine($"{elem} is already in the list");
    }
    public override void EditElem(string oldName, string newName, decimal newCost)
    {
        var elem = FindElem(oldName);
        if (elem == null)
        {
            Console.WriteLine($"Could not find base {oldName}.");
            return;
        }
        var classic = FindElem("классическое");
        if (classic == null)
        {
            Console.WriteLine($"Please, add 'классическое' base");
            return;
        }

        if (newCost > (decimal)0.2 * classic.Cost  &&  newName != "классическое")
        {
            Console.WriteLine($"{newName} is more than 20 percent of {classic.Cost} (классическое cost)");
            return;
        }
        if (FindElem(newName) != null && oldName != newName)
        {
            Console.WriteLine($"{newName} is already in the list");
            return;
        }
        elem.Update(newName, newCost);
        Elems[(int) FindIdxElem(newName)!] = elem;
        Console.WriteLine($"Updated {oldName} to {newName} with {newCost}");
    }
    
}
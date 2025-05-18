using WinFormsApp1.factories;

namespace WinFormsApp1.logic.repositories;

public class PizzaCrustRepository : Repository<PizzaCrust>
{
    public PizzaCrustRepository() : base(new PizzaCrustFactory())
    {
    }
    
    public void Update(string oldName, string newName, decimal newCost, List<Pizza> list, bool mode)
    {
        var select = GetByName(oldName);
        if (select == null)
            throw new KeyNotFoundException("Pizza not found");
        if (GetByName(newName) != null && oldName != newName)
            throw new Exception("Pizza already exists");
        select.Update(newName, newCost, list, mode);
    }
}
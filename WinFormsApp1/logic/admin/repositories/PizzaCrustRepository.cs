using WinFormsApp1.factories;

namespace WinFormsApp1.logic.repositories;

public class PizzaCrustRepository : Repository<PizzaCrust>
{
    protected PizzaCrustFactory _factory;
    public PizzaCrustRepository() : base(new PizzaCrustFactory())
    {
    }
    
    public void Add(string name, List<Ingredient> ingredients, List<Pizza> pizzas, bool mode)
    {
        var item = GetByName(name);
        if (item != null) 
            throw new KeyNotFoundException($"{item} is present in the repository");
        base.Add(_factory.Create(name, ingredients, pizzas, mode));
    }
    
    public void Update(string oldName, string newName, List<Ingredient> ingredients, List<Pizza> list, bool mode)
    {
        var select = GetByName(oldName);
        if (select == null)
            throw new KeyNotFoundException("Pizza not found");
        if (GetByName(newName) != null && oldName != newName)
            throw new Exception("Pizza already exists");
        select.Update(newName, ingredients, list, mode);
    }
}
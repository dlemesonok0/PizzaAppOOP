using WinFormsApp1.repositories;

namespace WinFormsApp1.factories;

public class PizzaBaseFactory : Factory<PizzaBase>
{
    private PizzaBaseRepository _repository;

    public PizzaBaseFactory(PizzaBaseRepository repository)
    {
        _repository = repository;
    }
    
    public override PizzaBase Create(string name, decimal cost)
    {
        if (name.ToLower() != "classic" && _repository.Classic == null) 
            throw new ArgumentException(nameof(_repository), " doesn't have a Classic base.");
        
        if (name.ToLower() != "classic" && cost > _repository.Classic.Cost * 1.2m)
            throw new ArgumentException(nameof(name), "Base cost is out of range 20% of the Classic base.");
        
        return new PizzaBase(name, cost);
    }    
}
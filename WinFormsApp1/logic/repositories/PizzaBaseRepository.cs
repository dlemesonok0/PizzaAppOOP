using WinFormsApp1.factories;

namespace WinFormsApp1.repositories;

public class PizzaBaseRepository : Repository<PizzaBase>
{
    public PizzaBaseRepository() : base(null!)
    {
        _factory = new PizzaBaseFactory(this);
    }
    public PizzaBase? Classic
    {
        get
        {
            return GetAll().FirstOrDefault(i => i.Name == "Classic", null);
        }
    }
    
    public override void Update(string name, BaseEntity entity)
    {
        _factory.Create(entity.Name, entity.Cost);
        // if (entity.Name.ToLower() != "classic" && Classic == null) 
        //     throw new ArgumentException(" doesn't have a Classic base.");
        //
        // if (entity.Name.ToLower() != "classic" && entity.Cost > Classic.Cost * 1.2m)
        //     throw new ArgumentException(nameof(name), "Base cost is out of range 20% of the Classic base.");
        
        base.Update(name, entity);
    }
}
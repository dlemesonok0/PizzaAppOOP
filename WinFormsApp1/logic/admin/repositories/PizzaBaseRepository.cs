using WinFormsApp1.factories;
using WinFormsApp1.logic.repositories;

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

    public virtual void Update(string name, string newName, decimal cost)
    {
        if (name == "Classic")
        {
            foreach (var other in GetAll().Where(i => i.Name != "Classic"))
            {
                if (other.Cost > cost * 1.2m)
                {
                    throw new ArgumentException("Other costs are related to this.");
                }
            }
        }
        base.Update(name, newName, cost);
    }
    public override void Update(string name, BaseEntity entity)
    {
        _factory.Create(entity.Name, entity.Cost);
        if (name == "Classic")
        {
            foreach (var other in GetAll().Where(i => i.Name != "Classic"))
            {
                if (other.Cost > entity.Cost * 1.2m)
                {
                    throw new ArgumentException("Other costs are related to this.");
                }
            }
        }
        
        base.Update(name, entity);
    }

    public virtual void Delete(string name)
    {
        if (name == "Classic")
            throw new ArgumentException("You cannot delete the classic base.");
        base.Delete(name);
    }
}
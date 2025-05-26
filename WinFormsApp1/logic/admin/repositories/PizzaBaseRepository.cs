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

    public virtual void Update(Guid id, string newName, decimal cost)
    {
        if (GetById(id).Name == "Classic")
        {
            foreach (var other in GetAll().Where(i => i.Name != "Classic"))
            {
                if (other.Cost > cost * 1.2m)
                {
                    throw new ArgumentException("Other costs are related to this.");
                }
            }
        }
        base.Update(id, newName, cost);
    }
    public override void Update(Guid id, BaseEntity entity)
    {
        _factory.Create(entity.Name, entity.Cost);
        if (GetById(id).Name == "Classic")
        {
            foreach (var other in GetAll().Where(i => i.Name != "Classic"))
            {
                if (other.Cost > entity.Cost * 1.2m)
                {
                    throw new ArgumentException("Other costs are related to this.");
                }
            }
        }
        
        base.Update(id, entity);
    }

    public virtual void Delete(Guid id)
    {
        if (GetById(id).Name == "Classic")
            throw new ArgumentException("You cannot delete the classic base.");
        base.Delete(id);
    }
}
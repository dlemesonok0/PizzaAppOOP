namespace WinFormsApp1;

public abstract class BaseEntity {
    public string Name { get; protected set; }
    public virtual decimal Cost { get; protected set; }
    
    public Guid Id { get; protected set; }

    public BaseEntity(string name, decimal cost, Guid? id = null)
    {
        Validate(name, cost);
        Id = (id == null) ? Guid.NewGuid() : (Guid)id;
        Name = name;
        Cost = cost;
    }
    
    public virtual void Update(string newName, decimal newCost)
    {
        Validate(newName, newCost);
        Name = newName;
        Cost = newCost;
    }

    private static void Validate(string name, decimal cost)
    {
        if (cost < 0)
            throw new ArgumentNullException(nameof(cost), " cannot be less than zero.");
    }
    
    public override string ToString()
    {
        return $"{Name} - ({Cost:C})";
    }

    public virtual BaseEntity Clone()
    {
        throw new NotImplementedException("Метод Clone должен быть переопределен в производном классе.");
    }
}
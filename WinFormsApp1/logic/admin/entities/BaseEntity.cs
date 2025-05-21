namespace WinFormsApp1;

public abstract class BaseEntity {
    public string Name { get; protected set; }
    public virtual decimal Cost { get; protected set; }
    
    public Guid Id { get; set; }

    public BaseEntity(string name, decimal cost)
    {
        Validate(name, cost);
        Id = Guid.NewGuid();
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
        // if (string.IsNullOrWhiteSpace(name)) 
        //     throw new ArgumentNullException(nameof(name), " cannot be null or empty.");
        if (cost < 0)
            throw new ArgumentNullException(nameof(cost), " cannot be less than zero.");
    }
    
    public override string ToString()
    {
        return $"{Name} - ({Cost:C})";
    }

    public abstract BaseEntity Clone();
}
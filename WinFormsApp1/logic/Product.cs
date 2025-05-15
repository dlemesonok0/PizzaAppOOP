namespace WinFormsApp1;

public class Product {
    public string Name { get; protected set; }
    public decimal Cost { get; protected set; }

    public Product(string name, decimal cost)
    {
        if (string.IsNullOrWhiteSpace(name)) 
            throw new ArgumentNullException(nameof(name), " cannot be null or empty.");
        if (cost < 0)
            throw new ArgumentNullException(nameof(cost), " cannot be less than zero.");
        Name = name;
        Cost = cost;
    }
    
    protected Product()
    {
        Name = "Unknown";
        Cost = 0m;
    }
    
    public void Update(string newName, decimal newCost)
    {
        if (string.IsNullOrWhiteSpace(newName)) 
            throw new ArgumentNullException(nameof(newName), " cannot be null or empty.");
        if (newCost < 0)
            throw new ArgumentNullException(nameof(newCost), " cannot be less than zero.");
        
        Name = newName;
        Cost = newCost;
    }
    
    public override string ToString()
    {
        return $"{Name} - ({Cost})";
    }
}
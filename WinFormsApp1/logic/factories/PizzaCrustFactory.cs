namespace WinFormsApp1.factories;

public class PizzaCrustFactory : Factory<PizzaCrust>
{
    public PizzaCrust Create(string name, decimal cost, List<Pizza> list, bool mode)
    {
        return new PizzaCrust(name, cost, list, mode);
    }

    public override PizzaCrust Create(string name, decimal cost)
    {
        return new PizzaCrust(name, cost, new List<Pizza>(), true);
    }
}
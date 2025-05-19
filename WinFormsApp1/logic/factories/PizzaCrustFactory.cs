namespace WinFormsApp1.factories;

public class PizzaCrustFactory : Factory<PizzaCrust>
{
    public PizzaCrust Create(string name, List<Ingredient> ingredients, List<Pizza> list, bool mode)
    {
        return new PizzaCrust(name, ingredients, list, mode);
    }

    public override PizzaCrust Create(string name, decimal cost)
    {
        return new PizzaCrust(name, new List<Ingredient>(), new List<Pizza>(), true);
    }
}
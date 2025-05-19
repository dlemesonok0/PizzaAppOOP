namespace WinFormsApp1.factories;

public class IngredientFactory : Factory<Ingredient>
{
    public override Ingredient Create(string name, decimal cost)
    {
        return new Ingredient(name, cost);
    }
}
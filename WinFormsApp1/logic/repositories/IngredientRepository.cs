using WinFormsApp1.factories;

namespace WinFormsApp1.repositories;

public class IngredientRepository : Repository<Ingredient>
{
    public IngredientRepository() : base(new IngredientFactory()) {}
}
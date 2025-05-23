using WinFormsApp1.factories;
using WinFormsApp1.logic.repositories;

namespace WinFormsApp1.repositories;

public class IngredientRepository : Repository<Ingredient>
{
    public IngredientRepository() : base(new IngredientFactory()) {}
}
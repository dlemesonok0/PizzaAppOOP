using System.Collections.Generic;

namespace WinFormsApp1;

public class Ingredient : Product
{
    public Ingredient(string name, decimal cost) : base(name, cost) {}
}

public class IngredientManager : Manager
{
}
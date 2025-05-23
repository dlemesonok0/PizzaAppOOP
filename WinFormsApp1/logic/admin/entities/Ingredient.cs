using System.Collections.Generic;

namespace WinFormsApp1;

public class Ingredient : BaseEntity
{
    public Ingredient(string name, decimal cost) : base(name, cost) {}

    public override Ingredient Clone()
    {
        return new Ingredient(Name, Cost);
    }
}
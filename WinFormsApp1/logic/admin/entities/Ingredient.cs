using System.Collections.Generic;

namespace WinFormsApp1;

public class Ingredient : BaseEntity
{
    public Ingredient(string name, decimal cost, Guid? id = null) : base(name, cost, id) {}

    public override Ingredient Clone()
    {
        return new Ingredient(Name, Cost, Id);
    }
}
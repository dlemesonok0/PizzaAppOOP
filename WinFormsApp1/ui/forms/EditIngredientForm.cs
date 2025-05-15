namespace WinFormsApp1.forms;

public partial class EditIngredientForm : EditForm<Ingredient>
{
    public EditIngredientForm(Ingredient ingredient) 
    {
        EntityName = ingredient.Name;
        EntityCost = ingredient.Cost;
        Entity = ingredient;
    }

    protected override void SaveEntity()
    {
        Entity = new Ingredient(EntityName, EntityCost);
    }
}
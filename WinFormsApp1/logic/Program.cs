namespace WinFormsApp1;

static class Program
{
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
        //
        // var ingredientManager = new IngredientManager();
        // var baseManager = new BaseManager();
        //
        // ingredientManager.AddElem("Tomato", 0.50m);
        // ingredientManager.AddElem("Cheese", 1.20m);
        //
        // ingredientManager.ShowAllElems();
        //
        // ingredientManager.EditElem("Cheese", "Mozzarella", 1.50m);
        // ingredientManager.RemoveElem("Tomato");
        //
        // ingredientManager.ShowAllElems();
        // Console.WriteLine();
        //
        // baseManager.AddElem("Classic", 100m);
        // baseManager.AddElem("Mozzarella", 1.50m);
        // baseManager.AddElem("Black", 120m);
        // baseManager.EditElem("Mozzarella", "Cheese", 1.50m);
        // baseManager.EditElem("Black", "Yellow", 120m);
        // baseManager.EditElem("Cheese", "Classic", 20m);
        // baseManager.ShowAllElems();
    }
}
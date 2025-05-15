using WinFormsApp1.repositories;

namespace WinFormsApp1;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var ingredientRepo = new IngredientRepository();
        var pizzaBaseRepo = new PizzaBaseRepository();
        var pizzaRepo = new PizzaRepository();

        ingredientRepo.Add("Сыр моцарелла", 50);
        ingredientRepo.Add("Пепперони", 30);
        pizzaBaseRepo.Add("Classic", 100);
        pizzaBaseRepo.Add("Толстое", 110);

        Application.Run(new MainForm(ingredientRepo, pizzaBaseRepo, pizzaRepo));
    }
}
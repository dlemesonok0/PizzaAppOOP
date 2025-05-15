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
        
        ingredientRepo.Add("Сыр моцарелла", 150);
        ingredientRepo.Add("Пепперони", 120);
        ingredientRepo.Add("Ветчина", 140);
        ingredientRepo.Add("Грибы", 90);
        ingredientRepo.Add("Оливки", 80);
        ingredientRepo.Add("Ананас", 70);
        ingredientRepo.Add("Лук", 30);
        ingredientRepo.Add("Перец", 30);
        ingredientRepo.Add("Бекон", 130);
        
        pizzaBaseRepo.Add("Classic", 100);
        pizzaBaseRepo.Add("Толстое", 110);
        pizzaBaseRepo.Add("Тонкое", 90);
        pizzaBaseRepo.Add("Чёрное", 120);
        pizzaBaseRepo.Add("С сыром по краю", 120);
        
        var classicBase = pizzaBaseRepo.GetByName("Classic");
        var mozzarella = ingredientRepo.GetByName("Сыр моцарелла");
        var pepperoni = ingredientRepo.GetByName("Пепперони");
        var ham = ingredientRepo.GetByName("Ветчина");
        var mushrooms = ingredientRepo.GetByName("Грибы");

        pizzaRepo.Add(new Pizza("Пепперони", classicBase, [mozzarella, pepperoni]));
        pizzaRepo.Add(new Pizza("Маргарита", classicBase, [mozzarella]));
        pizzaRepo.Add(new Pizza("Гавайская", classicBase, [mozzarella, ham, mushrooms]));

        Application.Run(new MainForm(ingredientRepo, pizzaBaseRepo, pizzaRepo));
    }
}
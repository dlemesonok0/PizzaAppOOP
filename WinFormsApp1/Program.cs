using WinFormsApp1.logic.repositories;
using WinFormsApp1.logic.user;
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
        var pizzaCrustRepo = new PizzaCrustRepository();
        var orderRepository = new OrderRepository();
        
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

        pizzaRepo.Add(new Pizza("Пепперони", classicBase, null, [mozzarella, pepperoni]));
        pizzaRepo.Add(new Pizza("Маргарита", classicBase, null,[mozzarella]));
        pizzaRepo.Add(new Pizza("Гавайская", classicBase, null, [mozzarella, ham, mushrooms]));
        
        pizzaCrustRepo.Add(new PizzaCrust("Сырный", [mozzarella, pepperoni], [pizzaRepo.GetByName("Пепперони")], true));
        pizzaCrustRepo.Add(new PizzaCrust("Острый", [mozzarella, ham], [pizzaRepo.GetByName("Маргарита"), pizzaRepo.GetByName("Пепперони")], false));
        pizzaCrustRepo.Add(new PizzaCrust("Вегетарианский", [mushrooms, pepperoni], [pizzaRepo.GetByName("Маргарита"), pizzaRepo.GetByName("Пепперони"), pizzaRepo.GetByName("Гавайская")], true));

        Application.Run(new MainForm(ingredientRepo, pizzaBaseRepo, pizzaRepo, pizzaCrustRepo, orderRepository));
    }
}
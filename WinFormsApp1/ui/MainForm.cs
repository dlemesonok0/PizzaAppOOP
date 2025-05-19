using WinFormsApp1.logic.repositories;
using WinFormsApp1.repositories;

namespace WinFormsApp1;

public partial class MainForm : Form
{
    private readonly IngredientRepository _ingredientRepo;
    private readonly PizzaBaseRepository _baseRepo;
    private readonly PizzaRepository _pizzaRepo;
    private readonly PizzaCrustRepository _pizzaCrustRepo;

    public MainForm(IngredientRepository ingredientRepo, PizzaBaseRepository baseRepo, PizzaRepository pizzaRepo, PizzaCrustRepository pizzaCrustRepo)
    {
        InitializeUI();
        _ingredientRepo = ingredientRepo;
        _baseRepo = baseRepo;
        _pizzaRepo = pizzaRepo;
        _pizzaCrustRepo = pizzaCrustRepo;
    }

    private void InitializeUI()
    {
        Text = "Управление пиццами";
        Width = 300;
        Height = 200;

        Button btnIngredients = new Button { Text = "Ингредиенты", Dock = DockStyle.Top };
        Button btnBases = new Button { Text = "Основы", Dock = DockStyle.Top };
        Button btnPizzas = new Button { Text = "Пиццы", Dock = DockStyle.Top };
        Button btnCrusts = new Button { Text = "Бортики", Dock = DockStyle.Top };

        btnIngredients.Click += (s, e) => OpenForm(new IngredientsForm(_ingredientRepo));
        btnBases.Click += (s, e) => OpenForm(new PizzaBasesForm(_baseRepo));
        btnPizzas.Click += (s, e) => OpenForm(new PizzasForm(_pizzaRepo, _baseRepo, _ingredientRepo));
        btnCrusts.Click += (s, e) => OpenForm(new PizzaCrustForm(_pizzaCrustRepo, _pizzaRepo, _ingredientRepo));
            
        Controls.Add(btnIngredients);
        Controls.Add(btnBases);
        Controls.Add(btnPizzas);
        Controls.Add(btnCrusts);
    }

    private void OpenForm(Form form)
    {
        form.ShowDialog();
    }
}
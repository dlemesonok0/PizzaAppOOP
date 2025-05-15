using WinFormsApp1.repositories;

namespace WinFormsApp1;

public partial class MainForm : Form
{
    private readonly IngredientRepository _ingredientRepo;
    private readonly PizzaBaseRepository _baseRepo;
    private readonly PizzaRepository _pizzaRepo;

    public MainForm(IngredientRepository ingredientRepo, PizzaBaseRepository baseRepo, PizzaRepository pizzaRepo)
    {
        InitializeUI();
        _ingredientRepo = ingredientRepo;
        _baseRepo = baseRepo;
        _pizzaRepo = pizzaRepo;
    }

    private void InitializeUI()
    {
        Text = "Управление пиццами";
        Width = 300;
        Height = 200;

        Button btnIngredients = new Button { Text = "Ингредиенты", Dock = DockStyle.Top };
        Button btnBases = new Button { Text = "Основы", Dock = DockStyle.Top };
        Button btnPizzas = new Button { Text = "Пиццы", Dock = DockStyle.Top };

        btnIngredients.Click += (s, e) => OpenForm(new IngredientsForm(_ingredientRepo));
        btnBases.Click += (s, e) => OpenForm(new PizzaBasesForm(_baseRepo));
        btnPizzas.Click += (s, e) => OpenForm(new PizzasForm(_pizzaRepo, _baseRepo, _ingredientRepo));

        Controls.Add(btnIngredients);
        Controls.Add(btnBases);
        Controls.Add(btnPizzas);
    }

    private void OpenForm(Form form)
    {
        form.ShowDialog();
    }
}
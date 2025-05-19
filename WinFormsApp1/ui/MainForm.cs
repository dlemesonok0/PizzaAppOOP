using WinFormsApp1.logic.repositories;
using WinFormsApp1.logic.user;
using WinFormsApp1.repositories;

namespace WinFormsApp1;

public partial class MainForm : Form
{
    private readonly IngredientRepository _ingredientRepo;
    private readonly PizzaBaseRepository _baseRepo;
    private readonly PizzaRepository _pizzaRepo;
    private readonly PizzaCrustRepository _pizzaCrustRepo;
    private readonly OrderRepository _orderRepo;

    public MainForm(IngredientRepository ingredientRepo, PizzaBaseRepository baseRepo, PizzaRepository pizzaRepo, PizzaCrustRepository pizzaCrustRepo, OrderRepository orderRepo)
    {
        InitializeUI();
        _ingredientRepo = ingredientRepo;
        _baseRepo = baseRepo;
        _pizzaRepo = pizzaRepo;
        _pizzaCrustRepo = pizzaCrustRepo;
        _orderRepo = orderRepo;
    }

    private void InitializeUI()
    {
        Text = "PizzaApp";
        Width = 300;
        Height = 200;

        Button btnAdmin = new Button { Text = "Админка", Dock = DockStyle.Top };
        Button btnUser = new Button { Text = "Юзер экспириенс", Dock = DockStyle.Top };
       

        btnAdmin.Click += (sender, args) => OpenForm(new Administration(_ingredientRepo, _baseRepo, _pizzaRepo, _pizzaCrustRepo));
        btnUser.Click += (sender, args) => OpenForm(new OrdersForm(_orderRepo, _pizzaRepo, _pizzaCrustRepo, new OrderService(_orderRepo)));
        Controls.Add(btnAdmin);
        Controls.Add(btnUser);
    }

    private void OpenForm(Form form)
    {
        form.ShowDialog();
    }
}
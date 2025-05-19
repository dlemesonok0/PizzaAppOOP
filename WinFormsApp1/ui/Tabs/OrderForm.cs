using WinFormsApp1.forms;
using WinFormsApp1.logic.repositories;
using WinFormsApp1.logic.user;
using WinFormsApp1.repositories;

namespace WinFormsApp1;
//TODO: почистить

public partial class OrdersForm : Tab<Order>
{
    private readonly OrderService _orderService;
    private readonly PizzaRepository _pizzaRepo;
    private readonly PizzaCrustRepository _pizzaCrustRepo;

    public OrdersForm(
        OrderRepository repo, 
        PizzaRepository pizzaRepo, 
        PizzaCrustRepository crustRepo,
        OrderService orderService) : base(repo, "Заказы")
    {
        _orderService = orderService;
        _pizzaRepo = pizzaRepo;
        _pizzaCrustRepo = crustRepo;
    }

    protected override void EditButton_Click(object sender, EventArgs e)
    {
        var selected = listBox.SelectedItem as Order;
        if (selected == null) return;

        using var form = new EditOrderForm(selected, _pizzaRepo, _pizzaCrustRepo, _orderService);
        if (form.ShowDialog() == DialogResult.OK)
        {
            LoadData();
        }
    }

    protected override void AddButton_Click(object sender, EventArgs e)
    {
        using var form = new EditOrderForm(new Order("Новый заказ"), _pizzaRepo, _pizzaCrustRepo, _orderService);
        if (form.ShowDialog() == DialogResult.OK)
        {
            _repo.Add(form._order);
            LoadData();
        }
    }

    protected override void DeleteButton_Click(object sender, EventArgs e)
    {
        var selected = listBox.SelectedItem as Order;
        if (selected == null) return;

        try
        {
            _repo.Delete(selected.Name);
            LoadData();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка удаления: {ex.Message}");
        }
    }
}
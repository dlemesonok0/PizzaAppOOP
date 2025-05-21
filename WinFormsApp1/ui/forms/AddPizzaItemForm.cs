using System.ComponentModel;
using WinFormsApp1.logic.repositories;
using WinFormsApp1.logic.user;
using WinFormsApp1.repositories;

namespace WinFormsApp1.forms;

public partial class AddPizzaItemForm : AddPizzaForm
{
    private ComboBox sizeComboBox;
    
    public AddPizzaItemForm(PizzaBaseRepository baseRepo, IngredientRepository ingredientRepo, PizzaCrustRepository pizzaCrustRepo) : base(baseRepo, ingredientRepo, pizzaCrustRepo)
    {
        InitializeComponent();
        sizeComboBox.DataSource = Enum.GetValues(typeof(SizePizza));
    }

    public new void InitializeComponent()
    {
        var sizeLabel = new Label { Text = "Размер:", Left = 20, Top = 100 };
        sizeComboBox = new ComboBox
        {
            Left = 120,
            Top = 100,
            Width = 200
        };
        
        Controls.Add(sizeLabel);
        Controls.Add(sizeComboBox);
    }
    protected override void SaveButton_Click(object sender, EventArgs e)
    {
        var selectedIngredients = new List<Ingredient>();
        for (int i = 0; i < ingredientsCheckedList.Items.Count; i++)
        {
            if (ingredientsCheckedList.GetItemChecked(i))
            {
                selectedIngredients.Add((Ingredient)ingredientsCheckedList.Items[i]);
            }
        }
            
        var selectedSize = (SizePizza)sizeComboBox.SelectedItem;

        var pizzaBase = (PizzaBase)baseComboBox.SelectedItem;
        PizzaCrust pizzaCrust;
        try
        {
            pizzaCrust = (PizzaCrust)crustComboBox.SelectedItem;
        }
        catch (Exception ex)
        {
            pizzaCrust = null;
        }
        try
        {
            Result = new OrderItem(new Pizza(EntityName, pizzaBase, pizzaCrust, selectedIngredients), pizzaCrust.Clone(), selectedSize, false);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        DialogResult = DialogResult.OK;
        Close();
    }
    
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new OrderItem? Result { get; set; }

    public new OrderItem? GetResult()
    {
        return Result;
    }
}

namespace WinFormsApp1.ui;

public class SectionPizza : Section
{
    private ComboBox _cmbBase;
    private CheckedListBox _cmbIngredients;
    public SectionPizza(PizzaManager pizzaManager, IngredientManager ingredientManager, BaseManager baseManager) : base("Пицца", pizzaManager)
    {
        _lst.Location = new Point(10, 230);
        
        _cmbBase = new ComboBox
        {
            Location = new Point(10, 130),
            Width = 150,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        _cmbBase.DataSource = baseManager.Elems;
        
        _cmbIngredients = new CheckedListBox
        {
            Location = new Point(170, 130),
            Width = 150,
            Height = 100,
        };
        _cmbIngredients.DataSource = ingredientManager.Elems;
        
        Controls.Add(_cmbBase);
        Controls.Add(_cmbIngredients);
    }
    
}
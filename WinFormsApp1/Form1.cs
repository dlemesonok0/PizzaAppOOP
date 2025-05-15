using WinFormsApp1.ui;

namespace WinFormsApp1;

public partial class Form1 : Form
{
    private IngredientManager _ingredientManager = new();
    private BaseManager _baseManager = new();
    private PizzaManager _pizzaManager = new();

    public Form1()
    {
        InitializeComponent();
        InitializeUI();
    }

    private void InitializeUI()
    {
        var tabIngredients = new Section("Ингредиенты", _ingredientManager);
        var tabBases = new Section("Основы", _baseManager);
        var tabPizzas = new SectionPizza(_pizzaManager, _ingredientManager, _baseManager);

        var tabControl = new TabControl();
        tabControl.Dock = DockStyle.Fill;
        
        tabControl.TabPages.Add(tabIngredients);
        tabControl.TabPages.Add(tabBases);
        tabControl.TabPages.Add(tabPizzas);

        Controls.Add(tabControl);
    }

    private void RefreshIngredientList(ListBox listBox)
    {
        listBox.DataSource = null;
        listBox.DataSource = _ingredientManager.Elems.Select(i => i.ToString()).ToList();
    }
    
    private void RefreshBaseList(ListBox listBox)
    {
        listBox.DataSource = null;
        listBox.DataSource = _baseManager.Elems.Select(i => i.ToString()).ToList();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        // throw new System.NotImplementedException();
    }
}
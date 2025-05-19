using WinFormsApp1.repositories;
// TODO: выровнять блоки в ui
namespace WinFormsApp1.forms;

public partial class AddPizzaCrustForm : AddForm<PizzaCrust>
{
    private readonly PizzaRepository _pizzaRepository;
    private readonly IngredientRepository _ingredientRepo;
    
    private ComboBox _modeComboBox;
    private CheckedListBox _pizzasCheckedList;
    private CheckedListBox _ingredientsCheckedList;
    public AddPizzaCrustForm(PizzaRepository pizzaRepo, IngredientRepository ingredientRepo)
    {
        _pizzaRepository = pizzaRepo;
        _ingredientRepo = ingredientRepo;
        InitializeComponent();
        LoadData();
    }
    
    private void InitializeComponent()
    {
        Controls.Clear();
        Height = 500;
        Width = 700;
        
        var nameLabel = new Label { Text = "Название:", Left = 20, Top = 20, Width = 80 };
        EntityNameTextBox.Left = 110;
        EntityNameTextBox.Top = 20;
        EntityNameTextBox.Width = 160;
            
        var ingredientsLabel = new Label { Text = "Ингредиенты:", Left = 400, Top = 100 };
        _ingredientsCheckedList = new CheckedListBox
        {
            Left = 400,
            Top = 140,
            Width = 180,
            Height = 200
        };

        var modeLabel = new Label { Text = "Режим совместимости:", Left = 20, Top = 140 };
        _modeComboBox = new ComboBox
        {
            Left = 130,
            Top = 140,
            Width = 200,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        _modeComboBox.Items.AddRange(new object[] { "Только разрешённые", "Все, кроме запрещённых" });
        _modeComboBox.SelectedIndex = 0;

        var pizzasLabel = new Label { Text = "Выберите пиццы:", Left = 20, Top = 180 };
        _pizzasCheckedList = new CheckedListBox
        {
            Left = 130,
            Top = 210,
            Width = 200,
            Height = 180
        };
        
        var saveButton = new Button { Text = "Сохранить", Left = 90, Top = 400, Width = 100 };
        saveButton.Click += SaveButton_Click;
            
        var cancelButton = new Button { Text = "Отмена", Left = 200, Top = 400, Width = 80 };
        cancelButton.Click += (s, e) => DialogResult = DialogResult.Cancel;

        Controls.Add(nameLabel);
        Controls.Add(EntityNameTextBox);
        Controls.Add(ingredientsLabel);
        Controls.Add(_ingredientsCheckedList);
        Controls.Add(modeLabel);
        Controls.Add(_modeComboBox);
        Controls.Add(pizzasLabel);
        Controls.Add(_pizzasCheckedList);
        Controls.Add(saveButton);
        Controls.Add(cancelButton);
    }
    
    private void LoadData()
    {
        foreach (var pizza in _pizzaRepository.GetAll())
        {
            _pizzasCheckedList.Items.Add(pizza);
        }
        foreach (var ingredient in _ingredientRepo.GetAll())
        {
            _ingredientsCheckedList.Items.Add(ingredient);
        }
    }
    
    protected override void SaveButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }
    
    public (string Text, List<Ingredient> Ingredients, List<Pizza> List, bool Mode) GetResult()
    {
        return DialogResult == DialogResult.OK ? (EntityName, EntityIngredients, EntityList, EntityMode) : (null, null, [], false);
    }
    
    private bool EntityMode
    {
        get { return _modeComboBox.SelectedIndex == 0;}
    }
    private List<Pizza> EntityList
    {
        get
        {
            var selectedIngredients = new List<Pizza>();
            for (int i = 0; i < _pizzasCheckedList.Items.Count; i++)
            {
                if (_pizzasCheckedList.GetItemChecked(i))
                {
                    selectedIngredients.Add((Pizza)_pizzasCheckedList.Items[i]);
                }
            }
            return selectedIngredients;
        }
    }

    private List<Ingredient> EntityIngredients
    {
        get
        {
            var selectedIngredients = new List<Ingredient>();
            for (int i = 0; i < _ingredientsCheckedList.Items.Count; i++)
            {
                if (_ingredientsCheckedList.GetItemChecked(i))
                {
                    selectedIngredients.Add((Ingredient)_ingredientsCheckedList.Items[i]);
                }
            }
            return selectedIngredients;
        }
    }
}
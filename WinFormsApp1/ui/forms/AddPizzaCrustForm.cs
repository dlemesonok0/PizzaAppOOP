using WinFormsApp1.repositories;

namespace WinFormsApp1.forms;

public partial class AddPizzaCrustForm : AddForm<PizzaCrust>
{
    private readonly PizzaRepository _pizzaRepository;
    
    private ComboBox _modeComboBox;
    private CheckedListBox _pizzasCheckedList;
    public AddPizzaCrustForm(PizzaRepository repo)
    {
        _pizzaRepository = repo;
        InitializeComponent();
        LoadData();
    }
    
    private void InitializeComponent()
    {
        Controls.Clear();
        Height = 500;
        Width = 500;
        
        var nameLabel = new Label { Text = "Название:", Left = 20, Top = 20, Width = 80 };
        EntityNameTextBox.Left = 110;
        EntityNameTextBox.Top = 20;
        EntityNameTextBox.Width = 160;
            
        var costLabel = new Label { Text = "Стоимость:", Left = 20, Top = 60, Width = 80 };
        CostNumericUpDown.Left = 110;
        CostNumericUpDown.Top = 60;
        CostNumericUpDown.Width = 100;
        CostNumericUpDown.DecimalPlaces = 2;
        CostNumericUpDown.Minimum = 0;
        CostNumericUpDown.Increment = 10;
        CostNumericUpDown.Maximum = 10000000;

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
        Controls.Add(costLabel);
        Controls.Add(CostNumericUpDown);
        Controls.Add(modeLabel);
        Controls.Add(_modeComboBox);
        Controls.Add(pizzasLabel);
        Controls.Add(_pizzasCheckedList);
        Controls.Add(saveButton);
        Controls.Add(cancelButton);
    }
    
    private void LoadData()
    {
        var allPizzas = _pizzaRepository.GetAll();
        foreach (var pizza in allPizzas)
        {
            _pizzasCheckedList.Items.Add(pizza);
        }
    }
    
    protected override void SaveButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }
    
    public (string Text, decimal Value, List<Pizza> List, bool Mode) GetResult()
    {
        return DialogResult == DialogResult.OK ? (EntityName, EntityCost, EntityList, EntityMode) : (null, 0, [], false);
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
}
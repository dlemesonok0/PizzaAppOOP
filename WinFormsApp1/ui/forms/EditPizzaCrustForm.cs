using WinFormsApp1.logic.repositories;
using WinFormsApp1.repositories;

namespace WinFormsApp1.forms;
// TODO: переписать красиво, когда будет время, а его не будет
public partial class EditPizzaCrustForm : EditForm<PizzaCrust>
{
    private readonly PizzaRepository _pizzaRepository;
    private readonly PizzaCrust _crust;
    
    private TextBox _nameTextBox;
    private NumericUpDown _costNumericUpDown;
    private ComboBox _modeComboBox;
    private CheckedListBox _pizzasCheckedList;
    
    public EditPizzaCrustForm(PizzaCrust crust, PizzaRepository repo)
    {
        EntityName = crust.Name;
        EntityCost = crust.Cost;
        Entity = crust;
        
        _crust = crust;
        _pizzaRepository = repo;

        InitializeComponent();
        LoadData();
    }
    
    private void InitializeComponent()
    {
        Controls.Clear();
        Text = "Редактировать бортик";
        Width = 400;
        Height = 500;
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        
        var nameLabel = new Label { Text = "Название:", Left = 20, Top = 20 };
        _nameTextBox = new TextBox
        {
            Left = 120,
            Top = 20,
            Width = 200
        };

        var costLabel = new Label { Text = "Стоимость:", Left = 20, Top = 60 };
        _costNumericUpDown = new NumericUpDown
        {
            Left = 120,
            Top = 60,
            Width = 100,
            DecimalPlaces = 2,
            Minimum = 0,
            Maximum = 1000
        };

        var modeLabel = new Label { Text = "Режим совместимости:", Left = 20, Top = 100 };
        _modeComboBox = new ComboBox
        {
            Left = 180,
            Top = 100,
            Width = 150,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        _modeComboBox.Items.AddRange(new object[] { "Только разрешённые", "Все, кроме запрещённых" });
        _modeComboBox.SelectedIndex = 0;

        var pizzasLabel = new Label { Text = "Выберите пиццы:", Left = 20, Top = 140 };
        _pizzasCheckedList = new CheckedListBox
        {
            Left = 120,
            Top = 170,
            Width = 220,
            Height = 200
        };

        var saveButton = new Button { Text = "Сохранить", Left = 100, Top = 390, Width = 100 };
        var cancelButton = new Button { Text = "Отмена", Left = 220, Top = 390, Width = 100 };

        saveButton.Click += (s, e) =>
        {
            DialogResult = DialogResult.OK;
            Close();
        };
        cancelButton.Click += (s, e) => DialogResult = DialogResult.Cancel;

        Controls.Add(nameLabel);
        Controls.Add(_nameTextBox);
        Controls.Add(costLabel);
        Controls.Add(_costNumericUpDown);
        Controls.Add(modeLabel);
        Controls.Add(_modeComboBox);
        Controls.Add(pizzasLabel);
        Controls.Add(_pizzasCheckedList);
        Controls.Add(saveButton);
        Controls.Add(cancelButton);
    }
    
    private void LoadData()
    {
        _nameTextBox.Text = _crust.Name;
        _costNumericUpDown.Value = _crust.Cost;

        _modeComboBox.SelectedIndex = _crust.UseWhiteList ? 0 : 1;
        
        foreach (var pizza in _pizzaRepository.GetAll())
        {
            _pizzasCheckedList.Items.Add(pizza, _crust.Compatibility.Any(i => i.Name == pizza.Name));
        }
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
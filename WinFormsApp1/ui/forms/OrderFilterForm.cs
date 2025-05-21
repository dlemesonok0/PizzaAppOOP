using WinFormsApp1.logic.admin.repositories;
using WinFormsApp1.logic.admin.specification;
using WinFormsApp1.logic.user;

namespace WinFormsApp1.forms;

using System;
using System.Linq;
using System.Windows.Forms;

public partial class OrderFilterForm : Form
{
    private ISpecification<Order> _spec;
    
    private CheckBox dateFilterCheckBox;
    private CheckBox costFilterCheckBox;
    private CheckBox ingredientFilterCheckBox;

    private DateTimePicker datePicker;
    private NumericUpDown costUpDown;
    private TextBox ingredientTextBox;
    
    private CheckBox nameFilterCheckBox;
    private TextBox nameTextBox;
    
    private CheckBox costMoreThanCheckBox;
    private NumericUpDown costMoreThanUpDown;

    public OrderFilterForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text = "Фильтр заказов";
        Width = 400;
        Height = 300;
        StartPosition = FormStartPosition.CenterParent;
        
        nameFilterCheckBox = new CheckBox { Text = "По названию", Left = 20, Top = 140 };
        nameTextBox = new TextBox { Left = 150, Top = 140, Width = 180 };
        
        dateFilterCheckBox = new CheckBox { Text = "По дате", Left = 20, Top = 20 };
        datePicker = new DateTimePicker
        {
            Left = 150,
            Top = 20,
            Width = 180,
            Format = DateTimePickerFormat.Short
        };

        costFilterCheckBox = new CheckBox { Text = "Цена меньше", Left = 20, Top = 60 };
        costUpDown = new NumericUpDown
        {
            Left = 150,
            Top = 60,
            Width = 100,
            Minimum = 0,
            Maximum = 10000,
            DecimalPlaces = 2
        };

        ingredientFilterCheckBox = new CheckBox { Text = "Содержит ингредиент", Left = 20, Top = 100 };
        ingredientTextBox = new TextBox
        {
            Left = 150,
            Top = 100,
            Width = 180
        };
        
        costMoreThanCheckBox = new CheckBox { Text = "Цена больше", Left = 20, Top = 180 };
        costMoreThanUpDown = new NumericUpDown
        {
            Left = 150,
            Top = 180,
            Width = 100,
            Minimum = 0,
            Maximum = 10000,
            DecimalPlaces = 2
        };

        var applyButton = new Button { Text = "Применить", Left = 100, Top = 220, Width = 100 };
        var cancelButton = new Button { Text = "Отмена", Left = 220, Top = 220, Width = 100 };

        applyButton.Click += ApplyButton_Click;
        cancelButton.Click += (s, e) => DialogResult = DialogResult.Cancel;

        Controls.Add(dateFilterCheckBox);
        Controls.Add(datePicker);
        Controls.Add(costFilterCheckBox);
        Controls.Add(costUpDown);
        Controls.Add(ingredientFilterCheckBox);
        Controls.Add(ingredientTextBox);
        Controls.Add(applyButton);
        Controls.Add(cancelButton);
        Controls.Add(nameFilterCheckBox);
        Controls.Add(nameTextBox);
        Controls.Add(costMoreThanCheckBox);
        Controls.Add(costMoreThanUpDown);
    }

    private void ApplyButton_Click(object sender, EventArgs e)
    {
        List<ISpecification<Order>> specs = new();

        if (dateFilterCheckBox.Checked)
        {
            specs.Add(new OrderOnDateSpecification(datePicker.Value.Date));
        }

        if (costFilterCheckBox.Checked && costUpDown.Value > 0)
        {
            specs.Add(new CostLessThanSpecification<Order>(costUpDown.Value));
        }

        if (ingredientFilterCheckBox.Checked && !string.IsNullOrWhiteSpace(ingredientTextBox.Text))
        {
            specs.Add(new OrderContainsIngredientSpecification(ingredientTextBox.Text));
        }

        if (nameFilterCheckBox.Checked && !string.IsNullOrWhiteSpace(nameTextBox.Text))
        {
            specs.Add(new NameContainsSpecification<Order>(nameTextBox.Text));
        }
        
        if (costMoreThanCheckBox.Checked && costMoreThanUpDown.Value > 0)
        {
            specs.Add(new CostMoreThanSpecification<Order>(costMoreThanUpDown.Value));
        }

        if (specs.Count == 0)
        {
            _spec = null;
        }
        else
        {
            _spec = specs.Aggregate((current, next) => new AndSpecification<Order>(current, next));
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    public ISpecification<Order> GetSpecification() => _spec;
}
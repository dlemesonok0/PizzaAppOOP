using WinFormsApp1.logic.admin.specification;

namespace WinFormsApp1.forms;

public partial class PizzaFilterForm : Form
{
    private ISpecification<Pizza> _spec;

    public PizzaFilterForm()
    {
        InitializeComponent();
    }

    private TextBox nameTextBox;
    private NumericUpDown costLessTextBox;
    private NumericUpDown costMoreTextBox;
    private TextBox costHasTextBox;

    private CheckBox nameFilterCheckBox;
    private CheckBox costLessFilterCheckBox;
    private CheckBox costMoreFilterCheckBox;
    private CheckBox costHasFilterCheckBox;

    private void InitializeComponent()
    {
        Text = "Фильтр пицц";
        Width = 350;
        Height = 240;
        StartPosition = FormStartPosition.CenterParent;

        nameFilterCheckBox = new CheckBox { Text = "Имя содержит", Left = 20, Top = 20 };
        nameTextBox = new TextBox { Left = 150, Top = 20, Width = 160 };

        costLessFilterCheckBox = new CheckBox { Text = "Цена меньше", Left = 20, Top = 60 };
        costLessTextBox = new NumericUpDown
        {
            Left = 150,
            Top = 60,
            Width = 100,
            DecimalPlaces = 2,
            Minimum = 0,
            Maximum = 10000
        };
        
        costMoreFilterCheckBox = new CheckBox { Text = "Цена больше", Left = 20, Top = 100 };
        costMoreTextBox = new NumericUpDown
        {
            Left = 150,
            Top = 100,
            Width = 100,
            DecimalPlaces = 2,
            Minimum = 0,
            Maximum = 10000
        };
        
        costHasFilterCheckBox = new CheckBox { Text = "Ингредиент", Left = 20, Top = 140 };
        costHasTextBox = new TextBox
        {
            Left = 150,
            Top = 140,
            Width = 100,
        };

        var applyButton = new Button { Text = "Применить", Left = 100, Top = 160, Width = 100 };
        var cancelButton = new Button { Text = "Отмена", Left = 220, Top = 160, Width = 100 };

        applyButton.Click += ApplyButton_Click;
        cancelButton.Click += (s, e) => DialogResult = DialogResult.Cancel;

        Controls.Add(nameFilterCheckBox);
        Controls.Add(nameTextBox);
        Controls.Add(costLessFilterCheckBox);
        Controls.Add(costMoreFilterCheckBox);
        Controls.Add(costLessTextBox);
        Controls.Add(costMoreTextBox);
        Controls.Add(applyButton);
        Controls.Add(cancelButton);
        Controls.Add(costHasTextBox);
        Controls.Add(costHasFilterCheckBox);
    }

    private void ApplyButton_Click(object sender, EventArgs e)
    {
        List<ISpecification<Pizza>> specs = new();

        if (nameFilterCheckBox.Checked && !string.IsNullOrWhiteSpace(nameTextBox.Text))
        {
            specs.Add(new NameContainsSpecification<Pizza>(nameTextBox.Text));
        }

        if (costLessFilterCheckBox.Checked && costLessTextBox.Value > 0)
        {
            specs.Add(new CostLessThanSpecification<Pizza>(costLessTextBox.Value));
        }

        if (costMoreFilterCheckBox.Checked && costMoreTextBox.Value > 0)
        {
            specs.Add(new CostMoreThanSpecification<Pizza>(costMoreTextBox.Value));
        }
        
        if (costHasFilterCheckBox.Checked && costHasTextBox.Text.Length > 0)
        {
            specs.Add(new PizzaHasIngredientSpecification(costHasTextBox.Text));
        }

        if (specs.Count == 0)
        {
            _spec = null;
        }
        else
        {
            _spec = specs.Aggregate((current, next) => new AndSpecification<Pizza>(current, next));
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    public ISpecification<Pizza> GetSpecification() => _spec;
}
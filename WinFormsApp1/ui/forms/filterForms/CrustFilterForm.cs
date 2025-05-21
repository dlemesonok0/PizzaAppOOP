using WinFormsApp1.logic.admin.specification;

namespace WinFormsApp1.forms;

public partial class CrustFilterForm : Form
{
    private ISpecification<PizzaCrust> _spec;

    public CrustFilterForm()
    {
        InitializeComponent();
    }

    private TextBox nameTextBox;
    private NumericUpDown costLessTextBox;
    private NumericUpDown costMoreTextBox;

    private CheckBox nameFilterCheckBox;
    private CheckBox costLessFilterCheckBox;
    private CheckBox costMoreFilterCheckBox;

    private void InitializeComponent()
    {
        Text = "Фильтр бортиков";
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
    }

    private void ApplyButton_Click(object sender, EventArgs e)
    {
        List<ISpecification<PizzaCrust>> specs = new();

        if (nameFilterCheckBox.Checked && !string.IsNullOrWhiteSpace(nameTextBox.Text))
        {
            specs.Add(new NameContainsSpecification<PizzaCrust>(nameTextBox.Text));
        }

        if (costLessFilterCheckBox.Checked && costLessTextBox.Value > 0)
        {
            specs.Add(new CostLessThanSpecification<PizzaCrust>(costLessTextBox.Value));
        }

        if (costMoreFilterCheckBox.Checked && costMoreTextBox.Value > 0)
        {
            specs.Add(new CostMoreThanSpecification<PizzaCrust>(costMoreTextBox.Value));
        }

        if (specs.Count == 0)
        {
            _spec = null;
        }
        else
        {
            _spec = specs.Aggregate((current, next) => new AndSpecification<PizzaCrust>(current, next));
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    public ISpecification<PizzaCrust> GetSpecification() => _spec;
}
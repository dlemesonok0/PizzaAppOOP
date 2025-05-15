namespace WinFormsApp1.forms;

public abstract class EditForm<T> : Form where T : BaseEntity
{
    protected T Entity { get; set; }

    protected TextBox EntityNameTextBox { get; set; } = new TextBox();
    protected NumericUpDown CostNumericUpDown { get; set; } = new NumericUpDown();

    protected EditForm()
    {
        Text = "Редактировать";
        Width = 300;
        Height = 200;
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        InitializeCommonControls();
    }

    private void InitializeCommonControls()
    {
        var nameLabel = new Label { Text = "Название:", Left = 20, Top = 20, Width = 80 };
        EntityNameTextBox.Left = 110;
        EntityNameTextBox.Top = 20;
        EntityNameTextBox.Width = 150;

        var costLabel = new Label { Text = "Стоимость:", Left = 20, Top = 60, Width = 80 };
        CostNumericUpDown.Left = 110;
        CostNumericUpDown.Top = 60;
        CostNumericUpDown.Width = 100;
        CostNumericUpDown.DecimalPlaces = 2;
        CostNumericUpDown.Minimum = 0;
        CostNumericUpDown.Increment = 10;
        CostNumericUpDown.Maximum = 10000000;

        var saveButton = new Button { Text = "Сохранить", Left = 90, Top = 100, Width = 100 };
        saveButton.Click += (s, e) =>
        {
            SaveEntity();
            DialogResult = DialogResult.OK;
            Close();
        };

        var cancelButton = new Button { Text = "Отмена", Left = 200, Top = 100, Width = 80 };
        cancelButton.Click += (s, e) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };

        Controls.Add(nameLabel);
        Controls.Add(EntityNameTextBox);
        Controls.Add(costLabel);
        Controls.Add(CostNumericUpDown);
        Controls.Add(saveButton);
        Controls.Add(cancelButton);
    }

    protected abstract void SaveEntity();

    public T GetResult()
    {
        return DialogResult == DialogResult.OK ? Entity : null;
    }

    protected string EntityName
    {
        get => EntityNameTextBox.Text;
        set => EntityNameTextBox.Text = value;
    }

    protected decimal EntityCost
    {
        get => CostNumericUpDown.Value;
        set => CostNumericUpDown.Value = value;
    }
}
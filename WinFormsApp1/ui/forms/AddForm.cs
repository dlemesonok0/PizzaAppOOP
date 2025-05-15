namespace WinFormsApp1.forms;

public partial class AddForm<T> : Form where T : BaseEntity
{
    private string _name;
        private decimal _cost;

        private TextBox EntityNameTextBox { get; } = new TextBox();
        private NumericUpDown CostNumericUpDown { get; } = new NumericUpDown();

        public AddForm(string title = "Добавить")
        {
            InitializeComponent();
            Text = title;
            Width = 320;
            Height = 200;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void InitializeComponent()
        {
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
            
            var saveButton = new Button { Text = "Сохранить", Left = 90, Top = 100, Width = 100 };
            saveButton.Click += SaveButton_Click;
            
            var cancelButton = new Button { Text = "Отмена", Left = 200, Top = 100, Width = 80 };
            cancelButton.Click += (s, e) => DialogResult = DialogResult.Cancel;

            Controls.Add(nameLabel);
            Controls.Add(EntityNameTextBox);
            Controls.Add(costLabel);
            Controls.Add(CostNumericUpDown);
            Controls.Add(saveButton);
            Controls.Add(cancelButton);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            _name = EntityNameTextBox.Text;
            _cost = CostNumericUpDown.Value;
            DialogResult = DialogResult.OK;
            Close();
        }
        
        public string EntityName => EntityNameTextBox.Text;
        public decimal EntityCost => CostNumericUpDown.Value;

        public (string? Text, decimal Value) GetResult()
        {
            return DialogResult == DialogResult.OK 
                ? (EntityNameTextBox.Text, CostNumericUpDown.Value) 
                : (null, 0);
        }
}
using WinFormsApp1.logic.repositories;
using WinFormsApp1.logic.user;
using WinFormsApp1.repositories;
using Size = System.Drawing.Size;

namespace WinFormsApp1.forms;

public partial class EditOrderForm : Form
    {
        public readonly Order _order;
        private readonly PizzaRepository _pizzaRepo;
        private readonly PizzaCrustRepository _crustRepo;
        private readonly PizzaBaseRepository _pizzaBaseRepo;
        private readonly IngredientRepository _ingredientRepo;

        private TextBox nameTextBox;
        private ListBox itemsListBox;
        private TextBox commentTextBox;
        private CheckBox scheduledCheckBox;
        private DateTimePicker datePicker;

        public EditOrderForm(
            Order order,
            PizzaRepository pizzaRepo,
            PizzaCrustRepository crustRepo,
            PizzaBaseRepository pizzaBaseRepo,
            IngredientRepository ingredientRepo)
        {
            _order = order ?? throw new ArgumentNullException(nameof(order));
            _pizzaRepo = pizzaRepo ?? throw new ArgumentNullException(nameof(pizzaRepo));
            _crustRepo = crustRepo ?? throw new ArgumentNullException(nameof(crustRepo));
            _pizzaBaseRepo = pizzaBaseRepo ?? throw new ArgumentNullException(nameof(pizzaBaseRepo));
            _ingredientRepo = ingredientRepo ?? throw new ArgumentNullException(nameof(ingredientRepo));

            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            Text = "Редактировать заказ";
            Width = 600;
            Height = 500;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            var nameLabel = new Label { Text = "Название:", Left = 20, Top = 20 };
            nameTextBox = new TextBox
            {
                Left = 120,
                Top = 20,
                Width = 300
            };

            var commentLabel = new Label { Text = "Комментарий:", Left = 20, Top = 60 };
            commentTextBox = new TextBox
            {
                Left = 120,
                Top = 60,
                Width = 300,
                Height = 60,
                Multiline = true
            };

            var itemsLabel = new Label { Text = "Пиццы в заказе:", Left = 20, Top = 140 };
            itemsListBox = new ListBox
            {
                Left = 120,
                Top = 170,
                Width = 300,
                Height = 120
            };

            var addPizzaButton = new Button { Text = "Добавить пиццу", Left = 120, Top = 300, Width = 120 };
            var removePizzaButton = new Button { Text = "Удалить пиццу", Left = 260, Top = 300, Width = 120 };

            addPizzaButton.Click += AddPizzaButton_Click;
            removePizzaButton.Click += RemovePizzaButton_Click;

            scheduledCheckBox = new CheckBox
            {
                Text = "Отложить заказ",
                Left = 20,
                Top = 340
            };
            scheduledCheckBox.CheckedChanged += (s, e) =>
            {
                datePicker.Enabled = scheduledCheckBox.Checked;
            };

            datePicker = new DateTimePicker
            {
                Left = 120,
                Top = 340,
                Width = 200,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy-MM-dd HH:mm",
                ShowUpDown = true, 
                Enabled = false
            };

            var saveButton = new Button { Text = "Сохранить", Left = 180, Top = 400, Width = 100 };
            var cancelButton = new Button { Text = "Отмена", Left = 300, Top = 400, Width = 100 };

            saveButton.Click += SaveButton_Click;
            cancelButton.Click += (s, e) => DialogResult = DialogResult.Cancel;

            Controls.Add(nameLabel);
            Controls.Add(nameTextBox);
            Controls.Add(commentLabel);
            Controls.Add(commentTextBox);
            Controls.Add(itemsLabel);
            Controls.Add(itemsListBox);
            Controls.Add(addPizzaButton);
            Controls.Add(removePizzaButton);
            Controls.Add(scheduledCheckBox);
            Controls.Add(datePicker);
            Controls.Add(saveButton);
            Controls.Add(cancelButton);
        }

        private void LoadData()
        {
            nameTextBox.Text = _order.Name;
            commentTextBox.Text = _order.Comment;
            scheduledCheckBox.Checked = _order.IsWaiting;
            if (_order.ScheduledTime.HasValue)
                datePicker.Value = _order.ScheduledTime.Value;

            itemsListBox.DataSource = new BindingSource { DataSource = _order.Items };
        }

        private void AddPizzaButton_Click(object sender, EventArgs e)
        {
            using var selectTypeForm = new SelectPizzaTypeForm(_pizzaRepo, _pizzaBaseRepo, _ingredientRepo, _crustRepo);
            if (selectTypeForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var selectedPizza = selectTypeForm.Result;
                    _order.AddItem(selectedPizza);
                    itemsListBox.DataSource = new BindingSource { DataSource = _order.Items };
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void RemovePizzaButton_Click(object sender, EventArgs e)
        {
            int index = itemsListBox.SelectedIndex;
            if (index >= 0 && index < _order.Items.Count)
            {
                _order.Items.RemoveAt(index);
                itemsListBox.DataSource = new BindingSource { DataSource = _order.Items };
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                _order.Update(
                    nameTextBox.Text,
                    commentTextBox.Text,
                    scheduledCheckBox.Checked ? datePicker.Value : (DateTime?)null
                );

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении заказа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
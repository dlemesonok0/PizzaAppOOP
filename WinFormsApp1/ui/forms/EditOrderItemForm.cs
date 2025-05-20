using WinFormsApp1.logic.repositories;
using WinFormsApp1.logic.user;
using WinFormsApp1.repositories;
using System.ComponentModel;

namespace WinFormsApp1.forms;

public partial class EditOrderItemForm : Form
    {
        private readonly PizzaRepository _pizzaRepo;
        private readonly PizzaCrustRepository _crustRepo;

        private ComboBox pizzaComboBox;
        private ComboBox crustComboBox;
        private ComboBox sizeComboBox;
        private CheckBox duplicateCheckBox;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public OrderItem Result { get; private set; }

        public EditOrderItemForm(
            PizzaRepository pizzaRepo,
            PizzaCrustRepository crustRepo)
        {
            _pizzaRepo = pizzaRepo;
            _crustRepo = crustRepo;

            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            pizzaComboBox.DataSource = _pizzaRepo.GetAll().ToList();
            // crustComboBox.DataSource = _crustRepo.GetAll().ToList();
            sizeComboBox.DataSource = Enum.GetValues(typeof(SizePizza));
        }

        private void InitializeComponent()
        {
            Text = "Добавить пиццу в заказ";
            Width = 400;
            Height = 300;
            StartPosition = FormStartPosition.CenterParent;

            var pizzaLabel = new Label { Text = "Пицца:", Left = 20, Top = 20 };
            pizzaComboBox = new ComboBox
            {
                Left = 120,
                Top = 20,
                Width = 200
            };

            var sizeLabel = new Label { Text = "Размер:", Left = 20, Top = 100 };
            sizeComboBox = new ComboBox
            {
                Left = 120,
                Top = 100,
                Width = 200
            };

            duplicateCheckBox = new CheckBox
            {
                Text = "Удвоить ингредиенты",
                Left = 120,
                Top = 140
            };

            var saveButton = new Button { Text = "Сохранить", Left = 120, Top = 200, Width = 100 };
            var cancelButton = new Button { Text = "Отмена", Left = 240, Top = 200, Width = 100 };

            saveButton.Click += SaveButton_Click;
            cancelButton.Click += (s, e) => DialogResult = DialogResult.Cancel;

            Controls.Add(pizzaLabel);
            Controls.Add(pizzaComboBox);
            // Controls.Add(crustLabel);
            Controls.Add(crustComboBox);
            Controls.Add(sizeLabel);
            Controls.Add(sizeComboBox);
            Controls.Add(duplicateCheckBox);
            Controls.Add(saveButton);
            Controls.Add(cancelButton);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var selectedPizza = (Pizza)pizzaComboBox.SelectedItem;
            // var selectedCrust = crustComboBox.SelectedItem as PizzaCrust;
            var selectedSize = (SizePizza)sizeComboBox.SelectedItem;
            var duplicate = duplicateCheckBox.Checked;

            if (selectedPizza == null)
            {
                MessageBox.Show("Выберите пиццу");
                return;
            }

            // if (selectedCrust != null && !selectedCrust.IsCompatibleWith(selectedPizza))
            // {
            //     MessageBox.Show("Этот бортик несовместим с выбранной пиццей");
            //     return;
            // }

            Result = new OrderItem(selectedPizza, selectedSize, duplicate);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
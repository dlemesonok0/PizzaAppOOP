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
            
            var crustLabel = new Label { Text = "Бортик:", Left = 20, Top = 60 };
            crustComboBox = new ComboBox
            {
                Left = 120,
                Top = 60,
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            // crustComboBox.DataSource = new BindingSource { DataSource = _crustRepo.GetAll() };
            crustComboBox.Items.Add("Без бортика");
            foreach (var crust in _crustRepo.GetAll())
                crustComboBox.Items.Add(crust);

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
            Controls.Add(crustLabel);
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
            var selectedCrust = crustComboBox.SelectedItem as PizzaCrust;
            var selectedSize = (SizePizza)sizeComboBox.SelectedItem;
            var duplicate = duplicateCheckBox.Checked;

            if (selectedPizza == null)
            {
                MessageBox.Show("Выберите пиццу");
                return;
            }

            try
            {
                Result = new OrderItem(selectedPizza.Clone(), (selectedCrust != null) ? selectedCrust.Clone() : null,
                    selectedSize, duplicate);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Close();
        }
    }
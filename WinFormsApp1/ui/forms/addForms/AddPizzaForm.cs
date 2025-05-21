using WinFormsApp1.repositories;
using System.ComponentModel;
using WinFormsApp1.logic.repositories;
using WinFormsApp1.logic.user;

namespace WinFormsApp1.forms;

public partial class AddPizzaForm : AddForm<Pizza>
    {
        protected readonly PizzaBaseRepository _baseRepo;
        protected readonly IngredientRepository _ingredientRepo;
        protected readonly PizzaCrustRepository _pizzaCrustRepo;

        protected ComboBox baseComboBox;
        protected CheckedListBox ingredientsCheckedList;
        protected ComboBox crustComboBox;
        protected ComboBox sizeComboBox;
        public SizePizza SelectedSize;
        
        public AddPizzaForm(
            PizzaBaseRepository baseRepo,
            IngredientRepository ingredientRepo,
            PizzaCrustRepository pizzaCrustRepo) : base("Добавить пиццу")
        {
            _baseRepo = baseRepo;
            _ingredientRepo = ingredientRepo;
            _pizzaCrustRepo = pizzaCrustRepo;

            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            Controls.Clear();
            Text = "Добавить пиццу";
            Width = 400;
            Height = 500;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            
            var nameLabel = new Label { Text = "Название:", Left = 20, Top = 20 };
            EntityNameTextBox = new TextBox
            {
                Left = 120,
                Top = 20,
                Width = 180
            };
            
            var baseLabel = new Label { Text = "Основа:", Left = 20, Top = 60 };
            baseComboBox = new ComboBox
            {
                Left = 120,
                Top = 60,
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            
            var ingredientsLabel = new Label { Text = "Ингредиенты:", Left = 20, Top = 100 };
            ingredientsCheckedList = new CheckedListBox
            {
                Left = 120,
                Top = 130,
                Width = 180,
                Height = 200
            };
            
            var crustLabel = new Label { Text = "Бортик:", Left = 20, Top = 340 };
            crustComboBox = new ComboBox
            {
                Left = 120,
                Top = 340,
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            
            var sizeLabel = new Label { Text = "Размер:", Left = 20, Top = 340 };
            sizeComboBox = new ComboBox
            {
                Left = 120,
                Top = 340,
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            
            var saveButton = new Button { Text = "Сохранить", Left = 100, Top = 400, Width = 100 };
            var cancelButton = new Button { Text = "Отмена", Left = 220, Top = 400, Width = 100 };

            saveButton.Click += SaveButton_Click;
            cancelButton.Click += (s, e) => DialogResult = DialogResult.Cancel;
            
            Controls.Add(nameLabel);
            Controls.Add(EntityNameTextBox);
            Controls.Add(baseLabel);
            Controls.Add(baseComboBox);
            Controls.Add(ingredientsLabel);
            Controls.Add(ingredientsCheckedList);
            Controls.Add(saveButton);
            Controls.Add(cancelButton);
            Controls.Add(crustLabel);
            Controls.Add(crustComboBox);
            Controls.Add(sizeLabel);
            Controls.Add(sizeComboBox);
        }

        private void LoadData()
        {
            var bases = _baseRepo.GetAll();
            baseComboBox.DataSource = bases;

            foreach (var ingredient in _ingredientRepo.GetAll())
            {
                ingredientsCheckedList.Items.Add(ingredient);
            }
            crustComboBox.Items.Add("Без бортика");
            foreach (var crust in _pizzaCrustRepo.GetAll())
            {
                crustComboBox.Items.Add(crust);
            }
            
            if (crustComboBox.Items.Count > 0)
                crustComboBox.SelectedIndex = 0;
            
            sizeComboBox.DataSource = Enum.GetValues(typeof(SizePizza));
            sizeComboBox.SelectedIndex = 1;
        }

        protected override void SaveButton_Click(object sender, EventArgs e)
        {
            var selectedIngredients = new List<Ingredient>();
            for (int i = 0; i < ingredientsCheckedList.Items.Count; i++)
            {
                if (ingredientsCheckedList.GetItemChecked(i))
                {
                    selectedIngredients.Add((Ingredient)ingredientsCheckedList.Items[i]);
                }
            }
            
            var selectedSize = (SizePizza)sizeComboBox.SelectedItem;

            var pizzaBase = (PizzaBase)baseComboBox.SelectedItem;
            PizzaCrust pizzaCrust;
            try
            {
                pizzaCrust = (PizzaCrust)crustComboBox.SelectedItem;
            }
            catch (Exception ex)
            {
                pizzaCrust = null;
            }
            try
            {
                Result = new Pizza(EntityName, pizzaBase, pizzaCrust, selectedIngredients);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DialogResult = DialogResult.OK;
            Close();
        }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Pizza? Result { get; private set; }

        public new Pizza? GetResult()
        {
            return Result;
        }
    }
    // TODO: переписать, чтобы возвращала поля, а не объект
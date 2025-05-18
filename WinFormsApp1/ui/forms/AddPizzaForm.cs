using WinFormsApp1.repositories;
using System.ComponentModel;

namespace WinFormsApp1.forms;

public partial class AddPizzaForm : AddForm<Pizza>
    {
        private readonly PizzaBaseRepository _baseRepo;
        private readonly IngredientRepository _ingredientRepo;

        private ComboBox baseComboBox;
        private CheckedListBox ingredientsCheckedList;

        public AddPizzaForm(
            PizzaBaseRepository baseRepo,
            IngredientRepository ingredientRepo) : base("Добавить пиццу")
        {
            _baseRepo = baseRepo;
            _ingredientRepo = ingredientRepo;

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
            
            var saveButton = new Button { Text = "Сохранить", Left = 100, Top = 350, Width = 100 };
            var cancelButton = new Button { Text = "Отмена", Left = 220, Top = 350, Width = 100 };

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
        }

        private void LoadData()
        {
            var bases = _baseRepo.GetAll();
            baseComboBox.DataSource = bases;

            foreach (var ingredient in _ingredientRepo.GetAll())
            {
                ingredientsCheckedList.Items.Add(ingredient);
            }
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

            var pizzaBase = (PizzaBase)baseComboBox.SelectedItem;
            try
            {
                Result = new Pizza(EntityName, pizzaBase, selectedIngredients);
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
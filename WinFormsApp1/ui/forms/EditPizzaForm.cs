using WinFormsApp1.repositories;
using System.ComponentModel;

namespace WinFormsApp1.forms;

public partial class EditPizzaForm : EditForm<Pizza>
    {
        private readonly PizzaBaseRepository _baseRepo;
        private readonly IngredientRepository _ingredientRepo;

        private readonly Pizza _pizza;

        public EditPizzaForm(
            Pizza pizza,
            PizzaBaseRepository baseRepo,
            IngredientRepository ingredientRepo)
        {
            _pizza = pizza;
            _baseRepo = baseRepo;
            _ingredientRepo = ingredientRepo;

            InitializeComponent();
            LoadData();
        }

        private ComboBox baseComboBox;
        private CheckedListBox ingredientsCheckedList;

        private void InitializeComponent()
        {
            Controls.Clear();
            Text = "Редактировать пиццу";
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
                Width = 200
            };

            var baseLabel = new Label { Text = "Основа:", Left = 20, Top = 60 };
            baseComboBox = new ComboBox
            {
                Left = 120,
                Top = 60,
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            
            var ingredientsLabel = new Label { Text = "Ингредиенты:", Left = 20, Top = 100 };
            ingredientsCheckedList = new CheckedListBox
            {
                Left = 120,
                Top = 130,
                Width = 200,
                Height = 200
            };

            var saveButton = new Button { Text = "Сохранить", Left = 100, Top = 350, Width = 100 };
            var cancelButton = new Button { Text = "Отмена", Left = 220, Top = 350, Width = 100 };

            saveButton.Click += (s, e) =>
            {
                SaveEntity();
                DialogResult = DialogResult.OK;
                Close();
            };
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
            EntityName = _pizza.Name;
            EntityCost = _pizza.Cost;

            var bases = _baseRepo.GetAll();
            baseComboBox.DataSource = bases;
            if (_pizza.pizzaBase != null)
                baseComboBox.SelectedItem = bases.FirstOrDefault(b => b.Name == _pizza.pizzaBase.Name);

            ingredientsCheckedList.Items.Clear();
            var allIngredients = _ingredientRepo.GetAll();
            foreach (var ingredient in allIngredients)
            {
                ingredientsCheckedList.Items.Add(ingredient, _pizza.pizzaIngredients.Any(i => i.Name == ingredient.Name));
            }
        }

        protected override void SaveEntity()
        {
            var selectedIngredients = new List<Ingredient>();
            for (int i = 0; i < ingredientsCheckedList.Items.Count; i++)
            {
                if (ingredientsCheckedList.GetItemChecked(i))
                {
                    selectedIngredients.Add((Ingredient)ingredientsCheckedList.Items[i]);
                }
            }

            _pizza.Update(EntityName, (PizzaBase)baseComboBox.SelectedItem, selectedIngredients);
            
            Entity = _pizza;
        }
    }
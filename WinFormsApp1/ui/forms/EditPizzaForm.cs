using WinFormsApp1.repositories;
using System.ComponentModel;
using WinFormsApp1.logic.repositories;

namespace WinFormsApp1.forms;

public partial class EditPizzaForm : EditForm<Pizza>
    {
        private readonly PizzaBaseRepository _baseRepo;
        private readonly IngredientRepository _ingredientRepo;
        private readonly PizzaCrustRepository _pizzaCrustRepo;

        private readonly Pizza _pizza;

        private ComboBox crustComboBox;
        
        public EditPizzaForm(
            Pizza pizza,
            PizzaBaseRepository baseRepo,
            IngredientRepository ingredientRepo,
            PizzaCrustRepository pizzaCrustRepo)
        {
            _pizza = pizza;
            _baseRepo = baseRepo;
            _ingredientRepo = ingredientRepo;
            _pizzaCrustRepo = pizzaCrustRepo;

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
            
            var crustLabel = new Label { Text = "Бортик:", Left = 20, Top = 340 };
            crustComboBox = new ComboBox
            {
                Left = 120,
                Top = 340,
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            var saveButton = new Button { Text = "Сохранить", Left = 100, Top = 400, Width = 100 };
            var cancelButton = new Button { Text = "Отмена", Left = 220, Top = 400, Width = 100 };

            saveButton.Click += (s, e) =>
            {
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
            Controls.Add(crustLabel);
            Controls.Add(crustComboBox);
            Controls.Add(saveButton);
            Controls.Add(cancelButton);
        }

        private void LoadData()
        {
            EntityName = _pizza.Name;
            EntityCost = _pizza.Cost;

            var bases = _baseRepo.GetAll();
            baseComboBox.DataSource = bases;
            if (_pizza.Base != null)
                baseComboBox.SelectedItem = bases.FirstOrDefault(b => b.Name == _pizza.Base.Name);

            ingredientsCheckedList.Items.Clear();
            var allIngredients = _ingredientRepo.GetAll();
            foreach (var ingredient in allIngredients)
            {
                ingredientsCheckedList.Items.Add(ingredient, _pizza.PizzaIngredients.Any(i => i.Name == ingredient.Name));
            }
            
            crustComboBox.Items.Add("Без бортика");
            foreach (var crust in _pizzaCrustRepo.GetAll())
            {
                crustComboBox.Items.Add(crust);
            }
            
            if (crustComboBox.Items.Count > 0)
                crustComboBox.SelectedIndex = 0;
            if (_pizza.Crust != null)
                crustComboBox.SelectedItem = _pizzaCrustRepo.GetAll().FirstOrDefault(c => c.Name == _pizza.Crust.Name);
        }
        
        public (string? Text, PizzaBase pizzaBase, PizzaCrust pizzaCrust, List<Ingredient> List) GetResult()
        {
            return DialogResult == DialogResult.OK ? (EntityName, EntityBase, EntityCrust,  EntityList) : (null, null, null, []);
        }

        private List<Ingredient> EntityList
        {
            get
            {
                var selectedIngredients = new List<Ingredient>();
                for (int i = 0; i < ingredientsCheckedList.Items.Count; i++)
                {
                    if (ingredientsCheckedList.GetItemChecked(i))
                    {
                        selectedIngredients.Add((Ingredient)ingredientsCheckedList.Items[i]);
                    }
                }
                return selectedIngredients;
            }
        }

        private PizzaBase EntityBase
        {
            get
            {
                return (PizzaBase)baseComboBox.SelectedItem;
            }
        }
        
        private PizzaCrust? EntityCrust
        {
            get
            {
                Object item = crustComboBox.SelectedItem;
                return (item is PizzaCrust crust) ? crust : null;
            }
        }
    }
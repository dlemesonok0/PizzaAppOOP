using WinFormsApp1.logic.repositories;
using WinFormsApp1.logic.user;
using WinFormsApp1.repositories;

namespace WinFormsApp1.forms;

public partial class EditCombinedPizzaForm : Form
    {
        private readonly PizzaRepository _pizzaRepo;
        private readonly PizzaCrustRepository _crustRepo;
        private readonly PizzaBase _base;
        private readonly IngredientRepository _ingredientRepo;
        private readonly PizzaBaseRepository _pizzaBaseRepo;
        
        public readonly CombinedPizza Pizza;
        private readonly PizzaBase _fixedBase;
        private bool _isNew = false;

        private TextBox nameTextBox;
        private ListBox pizzaPartsListBox;
        private CheckedListBox availablePizzasCheckedList;
        
        private ComboBox crustComboBox;
        private ComboBox baseComboBox;
        private ComboBox sizeComboBox;

        public EditCombinedPizzaForm(
            CombinedPizza pizza,
            PizzaRepository pizzaRepo,
            PizzaBaseRepository pizzaBaseRepo,
            PizzaCrustRepository crustRepo,
            PizzaBase baseFromOrder, 
            IngredientRepository ingredientRepo,
            PizzaBase fixedBase)
        {
            Pizza = pizza;
            _pizzaRepo = pizzaRepo;
            _base = baseFromOrder;
            _crustRepo = crustRepo;
            _ingredientRepo = ingredientRepo;
            _pizzaBaseRepo = pizzaBaseRepo;
            
            _fixedBase = fixedBase;

            InitializeComponent(pizzaBaseRepo, crustRepo);

            if (Pizza == null)
            {
                _isNew = true;
                Pizza = new CombinedPizza(new List<Pizza>(), _base, null);
            }

            LoadData();
        }

        private void InitializeComponent(IRepository<PizzaBase> baseRepo, IRepository<PizzaCrust> crustRepo)
        {
            Text = "Редактировать комбинированную пиццу";
            Width = 600;
            Height = 500;
            StartPosition = FormStartPosition.CenterParent;

            var nameLabel = new Label { Text = "Название:", Left = 20, Top = 20 };
            nameTextBox = new TextBox
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
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = baseRepo.GetAll(),
                DisplayMember = "Name"
            };
            baseComboBox.SelectedItem = _fixedBase;

            var crustLabel = new Label { Text = "Бортик:", Left = 20, Top = 100 };
            crustComboBox = new ComboBox
            {
                Left = 120,
                Top = 100,
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            crustComboBox.Items.Add("Без общего бортика");
            foreach (var crust in _crustRepo.GetAll())
            {
                crustComboBox.Items.Add(crust);
            }
            crustComboBox.SelectedIndex = 0;

            var selectLabel = new Label { Text = "Выберите части:", Left = 20, Top = 140 };
            availablePizzasCheckedList = new CheckedListBox
            {
                Left = 120,
                Top = 170,
                Width = 300,
                Height = 150
            };

            // var selectedPartsListBox = new ListBox
            // {
            //     Left = 120,
            //     Top = 330,
            //     Width = 300,
            //     Height = 80
            // };
            
            var addCustomPartButton = new Button { Text = "Создать свою часть", Left = 120, Top = 330, Width = 150 };
            addCustomPartButton.Click += AddCustomPartButton_Click;
            
            var sizeLabel = new Label { Text = "Размер:", Left = 20, Top = 140 };
            sizeComboBox = new ComboBox
            {
                Left = 120,
                Top = 140,
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = Enum.GetValues(typeof(SizePizza)),
            };

            var saveButton = new Button { Text = "Сохранить", Left = 120, Top = 420, Width = 100 };
            var cancelButton = new Button { Text = "Отмена", Left = 240, Top = 420, Width = 100 };

            saveButton.Click += SaveButton_Click;
            cancelButton.Click += (s, e) => DialogResult = DialogResult.Cancel;

            Controls.Add(nameLabel);
            Controls.Add(nameTextBox);
            Controls.Add(baseLabel);
            Controls.Add(baseComboBox);
            Controls.Add(crustLabel);
            Controls.Add(crustComboBox);
            Controls.Add(selectLabel);
            Controls.Add(availablePizzasCheckedList);
            Controls.Add(saveButton);
            Controls.Add(cancelButton);
            Controls.Add(sizeLabel);
            Controls.Add(sizeComboBox);
            Controls.Add(addCustomPartButton);
        }

        private void LoadData()
        {
            var allPizzas = _pizzaRepo.GetAll().Where(p => !(p is CombinedPizza));
            foreach (var pizza in allPizzas)
            {
                availablePizzasCheckedList.Items.Add(pizza, Pizza.Parts.Any(p => p.Name == pizza.Name));
            }

            if (Pizza.Crust != null)
            {
                crustComboBox.SelectedItem = Pizza.Crust;
            }
        }

        public SizePizza Size => (SizePizza) sizeComboBox.SelectedItem;

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (availablePizzasCheckedList.CheckedItems.Count < 2)
            {
                MessageBox.Show("Выберите хотя бы две пиццы");
                return;
            }
            
            var selectedPizzas = new List<Pizza>();
            foreach (var item in availablePizzasCheckedList.CheckedItems)
            {
                selectedPizzas.Add(((Pizza)item).Clone());
            }
            
            var selectedBase = (PizzaBase)baseComboBox.SelectedItem;
            
            var selectedCrust = crustComboBox.SelectedItem as PizzaCrust;
            
            Pizza.Update(nameTextBox.Text, selectedPizzas, selectedBase.Clone(), selectedCrust.Clone());
            DialogResult = DialogResult.OK;
            Close();
        }

        private void AddCustomPartButton_Click(object sender, EventArgs e)
        {
            using var form = new AddPizzaForm(_pizzaBaseRepo, _ingredientRepo, _crustRepo);
            if (form.ShowDialog() == DialogResult.OK)
            {
                availablePizzasCheckedList.Items.Add(form.Result);
            }
        }
    }
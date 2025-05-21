using WinFormsApp1.logic.repositories;
using WinFormsApp1.logic.user;
using WinFormsApp1.repositories;

namespace WinFormsApp1.forms;

public partial class SelectPizzaTypeForm : Form
    {
        private readonly PizzaRepository _pizzaRepo;
        private readonly PizzaBaseRepository _baseRepo;
        private readonly IngredientRepository _ingredientRepo;
        private readonly PizzaCrustRepository _crustRepo;
        

        private OrderItem _result;

        public OrderItem Result => _result;

        public SelectPizzaTypeForm(
            PizzaRepository pizzaRepo,
            PizzaBaseRepository baseRepo,
            IngredientRepository ingredientRepo,
            PizzaCrustRepository crustRepo)
        {
            _pizzaRepo = pizzaRepo;
            _baseRepo = baseRepo;
            _ingredientRepo = ingredientRepo;
            _crustRepo = crustRepo;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            Text = "Выберите тип пиццы";
            Width = 400;
            Height = 300;
            StartPosition = FormStartPosition.CenterParent;

            var label = new Label { Text = "Тип пиццы:", Left = 20, Top = 20 };

            var existingButton = new Button { Text = "Из списка", Left = 100, Top = 60, Width = 200 };
            var combinedButton = new Button { Text = "Комбинированная", Left = 100, Top = 100, Width = 200 };
            var customButton = new Button { Text = "Конструктор", Left = 100, Top = 140, Width = 200 };

            var cancelButton = new Button { Text = "Отмена", Left = 100, Top = 200, Width = 200 };

            existingButton.Click += ExistingButton_Click;
            combinedButton.Click += CombinedButton_Click;
            customButton.Click += CustomButton_Click;
            cancelButton.Click += (s, e) => DialogResult = DialogResult.Cancel;

            Controls.Add(label);
            Controls.Add(existingButton);
            Controls.Add(combinedButton);
            Controls.Add(customButton);
            Controls.Add(cancelButton);
        }

        private void ExistingButton_Click(object sender, EventArgs e)
        {
            using var form = new EditOrderItemForm(_pizzaRepo, _crustRepo);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _result = form.Result;
                DialogResult = DialogResult.OK;
                Close();
                Close();
            }
        }

        private void CombinedButton_Click(object sender, EventArgs e)
        {
            using var form = new EditCombinedPizzaForm(null, _pizzaRepo, _baseRepo, _crustRepo, null, _ingredientRepo, null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _result = new OrderItem(form.Pizza, null,  form.Size, false);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void CustomButton_Click(object sender, EventArgs e)
        {
            using var form = new AddPizzaItemForm(_baseRepo, _ingredientRepo, _crustRepo);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _result = form.Result;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
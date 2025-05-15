namespace WinFormsApp1.ui;

public abstract class ActionField : FlowLayoutPanel
{
    protected Manager _manager;
    
    protected TextBox _txtName;
    protected TextBox _txtCost;
    protected Button _btn;

    protected ActionField()
    {
    }
    public ActionField(string action)
    {
        InitializeField(action);
    }
    
    protected void InitializeField(string action)
    {
        var lblName = new Label
        {
            Text = "Имя:",
            AutoSize = true
        };

        var lblCost = new Label
        {
            Text = "Стоимость:",
            AutoSize = true
        };
        
        _txtName = new TextBox
        {
            Width = 100
        };

        _txtCost = new TextBox
        {
            Width = 100
        };

        _btn = new Button
        {
            Text = action,
            Size = new Size(100, 30)
        };
        _btn.Click += Btn_Click;
        
        Controls.Add(_btn);
        Controls.Add(lblName);
        Controls.Add(lblCost);
        Controls.Add(_txtName);
        Controls.Add(_txtCost);
    }

    protected virtual void Btn_Click(object sender, EventArgs e)
    {
    }
}

public class AddActionField : ActionField
{
    
    public AddActionField(Manager manager) : base("Добавить")
    {
        _manager = manager;
    }

    protected override void Btn_Click(object sender, EventArgs e)
    {
        decimal.TryParse(_txtCost.Text, out var cost);
        var name = _txtName.Text.Trim();

        _manager.AddElem(name, cost);

        _txtName.Clear();
        _txtCost.Clear();
    }
}
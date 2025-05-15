namespace WinFormsApp1.ui;

public class Section : TabPage
{
    protected TextBox _txtName;
    protected TextBox _txtCost;
    protected Button _btnAdd;
    
    protected ComboBox _cmbEdit;
    protected TextBox _txtNewName;
    protected TextBox _txtNewCost;
    protected Button _btnEdit;
    
    protected ListBox _lst;
    protected Manager _manager;
    
    protected ComboBox _cmbDelete;
    protected Button _btnDelete;

    protected Section()
    {
    }
    public Section(string text, Manager manager) : base(text)
    {
        Text = text;
        // InitializeAddBlock();
        Controls.Add(new AddActionField(_manager));
        InitializeEditBlock();
        InitializeDeleteBlock();
        _lst = new ListBox
        {
            Location = new Point(560, 10),
            Width = 540,
            Height = 200
        };
        
        _manager = manager;
        if (_cmbEdit != null) _cmbEdit.DataSource = manager.Elems;
        if (_lst != null) _lst.DataSource = manager.Elems;
        if (_cmbDelete != null) _cmbDelete.DataSource = manager.Elems;
    }

    private void InitializeAddBlock()
    {
        var lblName = new Label
        {
            Text = "Имя:",
            Location = new Point(10, 15),
            AutoSize = true
        };

        var lblCost = new Label
        {
            Text = "Стоимость:",
            Location = new Point(230, 15),
            AutoSize = true
        };
        
        _txtName = new TextBox
        {
            Location = new Point(120, 10),
            Width = 100
        };

        _txtCost = new TextBox
        {
            Location = new Point(340, 10),
            Width = 100
        };

        _btnAdd = new Button
        {
            Text = "Добавить",
            Location = new Point(450, 10),
            Size = new Size(100, 30)
        };
        _btnAdd.Click += BtnAdd_Click;

        _lst = new ListBox
        {
            Location = new Point(560, 10),
            Width = 540,
            Height = 200
        };

        Controls.Add(lblName);
        Controls.Add(lblCost);
        Controls.Add(_txtName);
        Controls.Add(_txtCost);
        Controls.Add(_btnAdd);
        Controls.Add(_lst);
    }

    private void InitializeEditBlock()
    {
        var lblSelect = new Label
        {
            Text = "Выберите элемент:",
            Location = new Point(10, 55),
            AutoSize = true
        };

        var lblNewName = new Label
        {
            Text = "Новое имя:",
            Location = new Point(120, 55),
            AutoSize = true
        };

        var lblNewCost = new Label
        {
            Text = "Новая стоимость:",
            Location = new Point(230, 55),
            AutoSize = true
        };

        _cmbEdit = new ComboBox
        {
            Location = new Point(10, 75),
            Width = 100
        };

        _txtNewName = new TextBox
        {
            Location = new Point(120, 75),
            Width = 100
        };

        _txtNewCost = new TextBox
        {
            Location = new Point(230, 75),
            Width = 100
        };

        _btnEdit = new Button
        {
            Text = "Изменить",
            Location = new Point(340, 75),
            Size = new Size(100, 30)
        };
        _btnEdit.Click += BtnEdit_Click;

        Controls.Add(lblSelect);
        Controls.Add(lblNewName);
        Controls.Add(lblNewCost);
        Controls.Add(_cmbEdit);
        Controls.Add(_txtNewName);
        Controls.Add(_txtNewCost);
        Controls.Add(_btnEdit);
    }
    
    private void InitializeDeleteBlock()
    {
        var lblDelete = new Label
        {
            Text = "Выберите для удаления:",
            Location = new Point(10, 110),
            AutoSize = true
        };

        _cmbDelete = new ComboBox
        {
            Location = new Point(10, 130),
            Width = 100
        };

        _btnDelete = new Button
        {
            Text = "Удалить",
            Location = new Point(120, 130),
            Size = new Size(100, 30)
        };
        _btnDelete.Click += BtnDelete_Click;

        Controls.Add(lblDelete);
        Controls.Add(_cmbDelete);
        Controls.Add(_btnDelete);
    }

    private void BtnAdd_Click(object sender, EventArgs e)
    {
        decimal.TryParse(_txtCost.Text, out var cost);
        var name = _txtName.Text.Trim();

        _manager.AddElem(name, cost);

        _txtName.Clear();
        _txtCost.Clear();
    }
    
    private void BtnEdit_Click(object sender, EventArgs e)
    {
        Product old = (Product) _cmbEdit.SelectedItem!;
        decimal.TryParse(_txtNewCost.Text, out var cost);
        var name = _txtNewName.Text.Trim();

        _manager.EditElem(old.Name, name, cost);

        _txtNewName.Clear();
        _txtNewCost.Clear();
    }
    
    private void BtnDelete_Click(object sender, EventArgs e)
    {
        Product elem = (Product) _cmbEdit.SelectedItem!;

        _manager.RemoveElem(elem.Name);
    }
}
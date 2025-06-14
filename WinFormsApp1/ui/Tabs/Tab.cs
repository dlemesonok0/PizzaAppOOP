using WinFormsApp1.forms;
using WinFormsApp1.logic.admin.specification;
using WinFormsApp1.logic.repositories;
using WinFormsApp1.repositories;

namespace WinFormsApp1;

public abstract partial class Tab<T> : Form where T : BaseEntity
{
    protected Repository<T> _repo;
    protected ListBox listBox;
    
    protected ISpecification<T> Specification;
    
    public Tab(Repository<T> repo, string tabName)
    {
        InitializeComponent(tabName);
        _repo = repo;
        LoadData();
    }

    protected void LoadData()
    {
        var data = Specification == null 
            ? _repo.GetAll() 
            : _repo.Find(Specification).ToList();
        
        listBox.DataSource = new BindingSource { DataSource = data };
    }

    private void InitializeComponent(string tabName)
    {
        Text = tabName;
        Width = 400;
        Height = 300;

        listBox = new ListBox { Dock = DockStyle.Fill };
        var panel = new Panel { Dock = DockStyle.Bottom, Height = 40 };

        var addButton = new Button { Text = "Добавить", Dock = DockStyle.Left };
        var editButton = new Button { Text = "Редактировать", Dock = DockStyle.Left };
        var deleteButton = new Button { Text = "Удалить", Dock = DockStyle.Right };

        panel.Controls.Add(addButton);
        panel.Controls.Add(editButton);
        panel.Controls.Add(deleteButton);

        Controls.Add(listBox);
        Controls.Add(panel);

        addButton.Click += AddButton_Click;
        editButton.Click += EditButton_Click;
        deleteButton.Click += DeleteButton_Click;
        
        var filterButton = new Button { Text = "Фильтр", Dock = DockStyle.Right };
        filterButton.Click += FilterButton_Click;

        Controls.Add(filterButton);
    }
    
    protected abstract void EditButton_Click(object sender, EventArgs e);
    protected abstract void AddButton_Click(object sender, EventArgs e);
    
    protected virtual void DeleteButton_Click(object sender, EventArgs e)
    {
        var selected = listBox.SelectedItem as BaseEntity;
        if (selected == null) return;
        try
        {
            _repo.Delete(selected.Id);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        LoadData();
    }
    
    protected abstract void FilterButton_Click(object sender, EventArgs e);
}
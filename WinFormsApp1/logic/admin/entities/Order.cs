using System.Data;

namespace WinFormsApp1.logic.user;

public class Order : BaseEntity
{
    public DateTime OrderTime { get; private set; }
    
    public string Comment { get; private set; }
    public DateTime? ScheduledTime { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();
    
    public override decimal Cost => Items.Sum(i => i.Cost);
    public override BaseEntity Clone()
    {
        throw new NotImplementedException();
    }

    public bool IsWaiting => ScheduledTime.HasValue;

    public Order(string name, Guid? id = null) : base(name, 0, id)
    {
        OrderTime = DateTime.Now;
    }

    public void Update(string name, string comment, DateTime? scheduledTime = null)
    {
        base.Update(name, 0);
        Comment = comment;
        ScheduledTime = scheduledTime;
        if (ScheduledTime != null && ScheduledTime.Value <= DateTime.Now)
        {
            throw new Exception("Отложенная доставка доступна только на будущее, пока в прошлое перемещать не умеем.");
        }
    }

    public void AddItem(OrderItem item)
    {
        if (item != null)
            Items.Add(item);
    }

    public void RemoveItem(OrderItem item)
    {
        Items.Remove(item);
    }
}
using System.Data;

namespace WinFormsApp1.logic.user;

public class Order : BaseEntity
{
    public DateTime OrderTime { get; set; }
    
    public string Comment { get; set; }
    public DateTime? ScheduledTime { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    
    public override decimal Cost => Items.Sum(i => i.Cost);
    public override BaseEntity Clone()
    {
        throw new NotImplementedException();
    }

    public bool IsWaiting => ScheduledTime.HasValue;

    public Order(string name) : base(name, 0)
    {
        Id = Guid.NewGuid();
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
        Items.Add(item);
    }

    public void RemoveItem(OrderItem item)
    {
        Items.Remove(item);
    }
}
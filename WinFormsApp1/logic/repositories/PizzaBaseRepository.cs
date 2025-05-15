namespace WinFormsApp1.repositories;

public class PizzaBaseRepository : Repository<PizzaBase>
{
    public PizzaBase? Classic
    {
        get
        {
            return GetAll().FirstOrDefault(i => i.Name == "Classic", null);
        }
    }
}
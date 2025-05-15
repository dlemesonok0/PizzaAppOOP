using WinFormsApp1.factories;

namespace WinFormsApp1.repositories;

public class PizzaRepository : Repository<Pizza>
{
    public PizzaRepository() : base(new PizzaFactory())
    {
    }
}
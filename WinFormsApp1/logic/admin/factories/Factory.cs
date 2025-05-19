namespace WinFormsApp1.factories;

public abstract class Factory<T> : IFactory<T> where T : BaseEntity
{
    public abstract T Create(string name, decimal cost);
}
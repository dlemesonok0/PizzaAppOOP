namespace WinFormsApp1.factories;

public interface IFactory<T> where T : BaseEntity
{
    T Create(string name, decimal cost);
}
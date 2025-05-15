namespace WinFormsApp1.repositories;

public interface IRepository<T> where T : class
{
    void Add(T item);
    void Update(string name, T item);
    void Delete(string name);
    T GetByName(string name);
    IEnumerable<T> GetAll();
}
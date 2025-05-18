namespace WinFormsApp1.repositories;

public interface IRepository<T> where T : class
{
    void Add(string name, decimal cost);
    void Update(string name, string newName, decimal cost);
    void Delete(string name);
    T? GetByName(string name);
    IEnumerable<T> GetAll();
}
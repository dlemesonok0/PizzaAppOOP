namespace WinFormsApp1.repositories;

public interface IRepository<T> where T : class
{
    void Add(string name, decimal cost);
    void Update(Guid id, string newName, decimal cost);
    void Delete(Guid id);
    T? GetByName(string name);
    T? GetById(Guid id);
    IEnumerable<T> GetAll();
}
namespace BusinessLogic.Interfaces;

public interface IRepository<T>
{
    public void Add(T entity);
    public List<T> GetAll();
    public void Update(T entity);
    public void Remove(T entity);
}
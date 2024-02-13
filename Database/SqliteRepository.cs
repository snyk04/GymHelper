using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class SqliteRepository<T>(DbSet<T> set) : IRepository<T>
    where T : class
{
    public void Add(T entity)
    {
        set.Add(entity);
    }

    public List<T> GetAll()
    {
        return set.ToList();
    }

    public void Update(T entity)
    {
        set.Update(entity);
    }

    public void Remove(T entity)
    {
        set.Remove(entity);
    }
}
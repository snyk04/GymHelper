﻿using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbSet<T> dbSet;
    private readonly ApplicationDbContext applicationDbContext;

    public Repository(DbSet<T> dbSet, ApplicationDbContext applicationDbContext)
    {
        this.dbSet = dbSet;
        this.applicationDbContext = applicationDbContext;
    }

    public List<T> GetList()
    {
        return dbSet.ToList();
    }

    public T Get(int id)
    {
        return dbSet.Find(id);
    }

    public void Add(T data)
    {
        dbSet.Add(data);
        applicationDbContext.SaveChanges();
    }

    public void Update(T data)
    {
        dbSet.Update(data);
        applicationDbContext.SaveChanges();
    }

    public void Remove(int id)
    {
        var dataToRemove = dbSet.Find(id);

        if (dataToRemove == null)
        {
            return;
        }

        Remove(dataToRemove);
    }
    
    public void Remove(T data)
    {
        dbSet.Remove(data);
        applicationDbContext.SaveChanges();
    }
}
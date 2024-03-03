namespace BusinessLogic.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> GetList();
        T Get(int id);
        void Add(T data);
        void Update(T data);
        void Remove(int id);
        void Remove(T data);
    }
}
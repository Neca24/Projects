namespace Core.Interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<bool> Add(T item);
        Task<bool> Update(T item);
        Task<bool> Delete(T item);
    }
}

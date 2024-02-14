namespace ClassLibrary.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class,BaseEntity, new()
    {
        Task<IEnumerable<T>> getAllAsync();
        Task<T> getByIdAsync(Guid id);
        Task addAsync(T entity);
        Task updateAsync(T entity);
        Task deleteAsync(Guid id);
    }
}

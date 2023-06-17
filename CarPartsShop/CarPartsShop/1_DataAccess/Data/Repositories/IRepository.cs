using CarPartsShop.Data.Entities;

namespace CarPartsShop.Data.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
        where T : class, IEntity
    {
        event EventHandler<T>? ItemAdded;

        event EventHandler<T>? ItemRemoved;
       
        event EventHandler<T>? FileSavedAdded;

        event EventHandler<T>? FileSavedRemoved;
    }
}

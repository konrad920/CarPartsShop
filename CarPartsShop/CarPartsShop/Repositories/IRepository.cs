using CarPartsShop.Entities;

namespace CarPartsShop.Repositories
{
    public interface IRepository<T> : IReadRepository<T>,IWriteRepository<T> 
        where T : class, IEntity
    {
    }
}

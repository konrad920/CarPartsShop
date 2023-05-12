using CarPartsShop.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarPartsShop.Repositories
{
    public class SQLPartsRepository <T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _carParts;

        public SQLPartsRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _carParts = _dbContext.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _carParts.ToList();
        }

        public T GetById(int id)
        {
            return _carParts.Find(id);
        }

        public void Add(T item)
        {
            _carParts.Add(item);
        } 

        public void Remove(T item)
        {
            _carParts.Remove(item);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}

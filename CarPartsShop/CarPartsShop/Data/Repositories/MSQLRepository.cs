using CarPartsShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarPartsShop.Data.Repositories
{
    public class MSQLRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly CarPartsDBContext _carPartsDBContext;
        private readonly DbSet<T> _carParts;
        public MSQLRepository(CarPartsDBContext carPartsDBContext) 
        {
            _carPartsDBContext = carPartsDBContext;
            _carParts = _carPartsDBContext.Set<T>();
        }
        public void Add(T item)
        {
            _carParts.Add(item);
        }

        public IEnumerable<T> GetAll()
        {
            return _carParts.ToList();
        }

        public T? GetById(int id)
        {
            return _carParts.Find(id);
        }

        public void Remove(T item)
        {
            _carParts.Remove(item);
        }

        public void Save()
        {
            _carPartsDBContext.SaveChanges();
        }
    }
}

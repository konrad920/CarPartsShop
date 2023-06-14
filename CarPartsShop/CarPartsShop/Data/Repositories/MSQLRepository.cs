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

        public event EventHandler<T>? ItemAdded;

        public event EventHandler<T>? ItemRemoved;

        public event EventHandler<T>? FileSavedAdded;

        public event EventHandler<T>? FileSavedRemoved;

        public event EventHandler<T>? ItemEdited;
        public void Add(T item)
        {
            _carParts.Add(item);
            ItemAdded?.Invoke(this, item);
            FileSavedAdded?.Invoke(this, item);
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
            ItemRemoved?.Invoke(this, item);
            FileSavedRemoved?.Invoke(this, item);
        }

        public void Save()
        {
            _carPartsDBContext.SaveChanges();
        }

        public void Edit()
        {

        }
    }
}

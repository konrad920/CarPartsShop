using CarPartsShop.Entities;

namespace CarPartsShop.Repositories
{
    public class CarPartsRepository <T> : IRepository<T>
        where T : class, IEntity, new()
    {
        protected readonly List<T> _carparts = new();

        public void Add(T item)
        {
            item.Id = _carparts.Count + 1;
            _carparts.Add(item);
        }

        public IEnumerable<T> GetAll()
        {
            return _carparts.ToArray();
        }

        public T GetById(int id)
        {
            var item = _carparts[id];
            return item;
        }

        public void Remove(T item)
        {
            _carparts.Remove(item);
            item.Id--;
        }

        public void Save()
        {

        }
    }
}

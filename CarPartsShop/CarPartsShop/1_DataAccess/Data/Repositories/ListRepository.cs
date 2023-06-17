using CarPartsShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Data.Repositories
{
    public class ListRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private readonly List<T> _items = new();

        public event EventHandler<T>? ItemAdded;

        public event EventHandler<T>? ItemRemoved;

        public event EventHandler<T>? FileSavedAdded;

        public event EventHandler<T>? FileSavedRemoved;
        public void Add(T item)
        {
            item.Id = _items.Count + 1;
            _items?.Add(item);
            ItemAdded?.Invoke(this, item);
            FileSavedAdded?.Invoke(this, item);
        }

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
        }

        public T? GetById(int id)
        {
            if (_items.Exists(x => x.Id == id))
            {
                return _items.Single(item => item.Id == id);
            }
            else { return null; }
        }

        public void Remove(T item)
        {
            _items?.Remove(item);
            ItemRemoved?.Invoke(this, item);
            FileSavedRemoved?.Invoke(this, item);
        }

        public void Save()
        {
            //foreach (var item in _items)
            //{
            //    Console.WriteLine(item);
            //}
        }
    }
}

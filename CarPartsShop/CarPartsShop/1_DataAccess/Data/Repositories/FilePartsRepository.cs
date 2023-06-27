using CarPartsShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace CarPartsShop.Data.Repositories
{
    public class FilePartsRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private string fileName = "CarParts.json";

        private readonly List<T> _items = new ();

        public event EventHandler<T>? ItemAdded;

        public event EventHandler<T>? ItemRemoved;

        public event EventHandler<T>? FileSavedAdded;

        public event EventHandler<T>? FileSavedRemoved;


        public void Add(T item)
        {
            _items.Add(item);
            ItemAdded?.Invoke(this, item);
            FileSavedAdded?.Invoke(this, item);
        }


        public void Remove(T item)
        {
            GetAll();
            _items.Remove(item);
            ItemRemoved?.Invoke(this, item);
            FileSavedRemoved?.Invoke(this, item);
        }

        public IEnumerable<T> GetAll()
        {
            if (File.Exists(fileName))
            {
                var serializedData = File.ReadAllText(fileName);
                var deserializedData = JsonSerializer.Deserialize<IEnumerable<T>>(serializedData);
                if (deserializedData != null)
                {
                    foreach (var item in deserializedData)
                    {
                        _items.Add(item);
                    }
                }
                return _items;
            }
            else
            {
                File.Create(fileName); 
                return null;
            }
        }

        public T? GetById(int id)
        {
            if (_items.Exists(x => x.Id == id))
            {
                return _items.Single(item => item.Id == id);
            }
            else { return null; }
        }

        public void Save()
        {
            File.Delete(fileName);
            var serializedData = JsonSerializer.Serialize<IEnumerable<T>>(_items);
            File.WriteAllText(fileName, serializedData);
        }
    }
}

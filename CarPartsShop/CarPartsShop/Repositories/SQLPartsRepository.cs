using CarPartsShop.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace CarPartsShop.Repositories
{
    //public delegate void ItemAdded<in T>(T item);
    public class SQLPartsRepository <T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _carParts;
        private string fileName = "CarParts.txt";
        //private readonly Action<T>? _itemAddedCallBack;

        public SQLPartsRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _carParts = _dbContext.Set<T>();
            //_itemAddedCallBack = itemAddedCallBack;
        }

        public event EventHandler<T>? ItemAdded;

        public event EventHandler<T>? ItemRemoved;

        public event EventHandler<T>? FileSavedAdded;

        public event EventHandler<T>? FileSavedRemoved;
        public IEnumerable<T> GetAll()
        {
            return _carParts.ToList();
        }

        public Dictionary<int, string> GetAll1()
        {
            if (File.Exists(fileName))
            {
                var linesFromFile = new Dictionary<int, string>();
                using (var reader = File.OpenText(fileName))
                {
                    var line = reader.ReadLine();
                    int i = 1;
                    while (line != null)
                    {
                        linesFromFile.Add(i, line);
                        line = reader.ReadLine();
                        i++;
                    }
                }
                return linesFromFile;
            }
            else
            {
                throw new Exception("File does not exist");
            }
        }

        public string GetById1(int id)
        {
            if (File.Exists(fileName))
            {
                var carParts = GetAll1();
                var carPart = carParts[id];
                return carPart;
            }
            else
            {
                throw new Exception("File does not exist");
            }
        }
        public T? GetById(int id)
        {
            return _carParts.Find(id);
        }

        public void Add(T item)
        {
            _carParts.Add(item);
            using (var writer = File.AppendText(fileName))
            {
                writer.WriteLine(item);
            }
            //_itemAddedCallBack?.Invoke(item);
            ItemAdded?.Invoke(this, item);
            FileSavedAdded?.Invoke(this, item);
        } 


        public void Remove1(T item)
        {
            if (File.Exists(fileName))
            {
                var carParts = GetAll1();
                carParts.Remove(item.Id);
                File.Delete(fileName);
                using (var writer = File.AppendText(fileName))
                {
                    foreach (var part in carParts)
                    {
                        writer?.WriteLine(part.Value);
                    }
                }
                ItemRemoved?.Invoke(this, item);
                FileSavedRemoved?.Invoke(this, item);
            }
            else
            {
                throw new Exception("File does not exist");
            }
        }
        public void Remove(T item)
        {
            _carParts.Remove(item);
            ItemRemoved?.Invoke(this, item);
            FileSavedRemoved?.Invoke(this, item);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}

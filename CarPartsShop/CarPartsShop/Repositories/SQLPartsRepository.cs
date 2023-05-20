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
        public IEnumerable<T> GetAll()
        {
            return _carParts.ToList();
        }

        public List<string> GetAll1()
        {
            if (File.Exists(fileName))
            {
                var linesFromFile = new List<string>();
                using (var reader = File.OpenText(fileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        linesFromFile.Add(line);
                        line = reader.ReadLine();
                    }
                }
                return linesFromFile;
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
        } 


        public void Remove1(T item)
        {
            if (File.Exists(fileName))
            {
                var carParts = GetAll1();
                carParts.RemoveAt(item.Id-1);
                File.Delete(fileName);
                using (var writer = File.AppendText(fileName))
                {
                    foreach (var part in carParts)
                    {
                        writer?.WriteLine(part);
                    }
                }
                ItemRemoved?.Invoke(this, item);
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
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}

using CarPartsShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace CarPartsShop.Data.Repositories
{
    public class FilePartsRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private string fileName = "CarParts.txt";

        public FilePartsRepository(DbContext dbContext)
        {
        }

        public event EventHandler<T>? ItemAdded;

        public event EventHandler<T>? ItemRemoved;

        public event EventHandler<T>? FileSavedAdded;

        public event EventHandler<T>? FileSavedRemoved;


        public Dictionary<int, string> GetAll()
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

        public string GetById(int id)
        {
            if (File.Exists(fileName))
            {
                var carParts = GetAll();
                var carPart = carParts[id];
                return carPart;
            }
            else
            {
                throw new Exception("File does not exist");
            }
        }

        public void Add(T item)
        {
            using (var writer = File.AppendText(fileName))
            {
                writer.WriteLine(item);
            }
            ItemAdded?.Invoke(this, item);
            FileSavedAdded?.Invoke(this, item);
        }


        public void Remove(T item)
        {
            if (File.Exists(fileName))
            {
                var carParts = GetAll();
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

        IEnumerable<T> IReadRepository<T>.GetAll()
        {
            throw new NotImplementedException();
        }

        T? IReadRepository<T>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}

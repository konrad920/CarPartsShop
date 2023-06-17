using CarPartsShop.Components.CsvReader.Extensions;
using CarPartsShop.Components.CsvReader.Models;
using CarPartsShop.Data.Entities;

namespace CarPartsShop.Components.CsvReader
{
    public class CsvReader : ICsvReader
    {
        public List<Car> CarProcess(string filePath)
        {
            if(!File.Exists(filePath))
            {
                return new List<Car>();
            }

            var cars = File.ReadAllLines(filePath)
                .Skip(1)
                .Where(x => x.Length > 0)
                .ToCar()
                .ToList();
            return cars;
        }

        public List<Manufacturer> ManufacturerProcess(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Manufacturer>();
            }

            var manufacturers = File.ReadAllLines(filePath)
                .Where(x => x.Length > 0)
                .Select(x =>
                {
                    var columns = x.Split(',');
                    return new Manufacturer
                    {
                        Name = columns[0],
                        Country = columns[1],
                        Year = int.Parse(columns[2])
                    };
                })
                .ToList();

            return manufacturers;
        }

        public List<CarParts> CarPartsProcess(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<CarParts>();
            }

            var carParts = File.ReadAllLines(filePath)
                .Where(x => x.Length > 0)
                .Select(x =>
                {
                    var columns = x.Split(',');
                    return new CarParts
                    {
                        Id = int.Parse(columns[0]),
                        NameOfPart = columns[1],
                        IsUsed = columns[2],
                        Price = decimal.Parse(columns[3]),
                        ModelOfCar = columns[5],
                        Sales = int.Parse(columns[6])
                    };
                })
                .ToList();

            return carParts;
        }
    }
}

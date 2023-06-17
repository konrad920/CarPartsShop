using CarPartsShop.Components.CsvReader.Models;
using System.Globalization;

namespace CarPartsShop.Components.CsvReader.Extensions
{
    public static class CarExtension
    {
        public static List<Car> ToCar1(this IEnumerable<string> source)
        {
            var list = new List<Car>();
            foreach (var line in source)
            {
                var car = new Car();
                var columns = line.Split(',');
                car.Year = int.Parse(columns[0]);
                car.Manufacturer = columns[1];
                car.Name = columns[2];
                car.Displacement = double.Parse(columns[3]);
                car.Cylinders = int.Parse(columns[4]);
                car.City = int.Parse(columns[5]);
                car.Highway = int.Parse(columns[6]);
                car.Combined = int.Parse(columns[7]);
                list.Add(car);
            }
            return list;
        }

        public static IEnumerable<Car> ToCar(this IEnumerable<String> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');
                yield return new Car
                { 
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3], CultureInfo.InvariantCulture),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}

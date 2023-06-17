using CarPartsShop.Components.CsvReader.Models;
using CarPartsShop.Data.Entities;

namespace CarPartsShop.Components.CsvReader
{
    public interface ICsvReader
    {
        List<Car> CarProcess(string filePath);

        List<Manufacturer> ManufacturerProcess(string filePath);

        List<CarParts> CarPartsProcess(string filePath);
    }
}

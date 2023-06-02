using CarPartsShop.Components.CsvReader.Models;

namespace CarPartsShop.Components.CsvReader
{
    public interface ICsvReader
    {
        List<Car> CarProcess(string filePath);

        List<Manufacturer> ManufacturerProcess(string filePath);
    }
}

using CarPartsShop.Entities;

namespace CarPartsShop.DataProvider
{
    public interface ICarPartsProvider
    {
        List<CarParts> FilterCarParts(decimal minPrice);

        List<string> GetUniqueModelOfCars();

        decimal GetMinimalPriceOfCarParts();
    }
}

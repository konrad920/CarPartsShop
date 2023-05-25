using CarPartsShop.Entities;

namespace CarPartsShop.DataProvider
{
    public interface ICarPartsProvider
    {
        List<string> GetUniqueModelOfCars();

        decimal GetMinimalPriceOfCarParts();

        List<CarParts> OrderByName();

        List<CarParts> OrderByPrice();

        List<CarParts> OrderByNameDescending();

        List<CarParts> OrderByNameAndModel();

        List<CarParts> WhereNameStartsWith(string prefix);

        List<CarParts> WhereNameStartsWithAndPriceIsGreaterThan(string prefix, decimal minPrice);

        List<CarParts> WhereModelIs(string model);

        CarParts FirstByModel(string model);

        CarParts LastOrDefoultByModel(string model);

        CarParts FirstOrDefoultByModelWithDefoult(string model);

        List<CarParts> TakeCarParts(int howMany);

        List<CarParts> TakeCarPartsByRange(Range range);

        List<CarParts> TakeCarPartsWhileStartsWith(string prefix);

        List<CarParts> SkipCarParts(int howMany);

        List<CarParts> SkipCarPartsWhileStartsWith(string prefix);

        List<CarParts> DistinctByModel();

        List<CarParts[]> ChunkyCarParts(int size);
    }
}

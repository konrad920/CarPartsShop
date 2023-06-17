

using CarPartsShop.Data.Entities;
using CarPartsShop.Data.Repositories;

namespace CarPartsShop.Components.DataProvider
{
    public class CarPartsProvider : ICarPartsProvider
    {
        private readonly IRepository<CarParts> _carPartsRepository;
        public CarPartsProvider(IRepository<CarParts> carPartsRepository)
        {
            _carPartsRepository = carPartsRepository;
        }


        public List<CarParts[]> ChunkyCarParts(int size)
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts.Chunk(size).ToList();
        }

        public List<CarParts> DistinctByModel()
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts
                .DistinctBy(x => x.ModelOfCar)
                .OrderBy(x => x.Id)
                .ToList();
        }

        public CarParts FirstByModel(string model)
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts.First(x => x.ModelOfCar == model);
        }

        public CarParts FirstOrDefoultByModelWithDefoult(string model)
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts.FirstOrDefault(
                x => x.ModelOfCar == model,
                new CarParts { Id = -1, NameOfPart = "NOT FOUND" });
        }

        public decimal GetMinimalPriceOfCarParts()
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts.Select(x => x.Price).Min();
        }

        public List<string> GetUniqueModelOfCars()
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts.Select(x => x.ModelOfCar).Distinct().ToList();
        }

        public CarParts? LastOrDefoultByModel(string model)
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts.LastOrDefault(x => x.ModelOfCar == model);
        }

        public List<CarParts> OrderByName()
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts.OrderBy(x => x.NameOfPart).ToList();
        }

        public List<CarParts> OrderByNameAndModel()
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts
                .OrderBy(x => x.NameOfPart)
                .ThenBy(x => x.ModelOfCar)
                .ToList();
        }

        public List<CarParts> OrderByNameDescending()
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts.OrderByDescending(x => x.NameOfPart).ToList();
        }

        public List<CarParts> OrderByPrice()
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts.OrderBy(x => x.Price).ToList();
        }

        public List<CarParts> SkipCarParts(int howMany)
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts
                .OrderBy(x => x.NameOfPart)
                .Skip(howMany)
                .ToList();
        }

        public List<CarParts> SkipCarPartsWhileStartsWith(string prefix)
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts
                .OrderBy(x => x.NameOfPart)
                .SkipWhile(x => x.NameOfPart.StartsWith(prefix))
                .ToList();
        }

        public List<CarParts> TakeCarParts(int howMany)
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts
                .OrderBy(x => x.NameOfPart)
                .Take(howMany)
                .ToList();
        }

        public List<CarParts> TakeCarPartsByRange(Range range)
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts
                .OrderBy(x => x.NameOfPart)
                .Take(range)
                .ToList();
        }

        public List<CarParts> TakeCarPartsWhileStartsWith(string prefix)
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts
                .OrderBy(x => x.NameOfPart)
                .TakeWhile(x => x.NameOfPart.StartsWith(prefix))
                .ToList();
        }

        public List<CarParts> WhereModelIs(string model)
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts.Where(x => x.ModelOfCar == model).ToList();
        }

        public List<CarParts> WhereNameStartsWith(string prefix)
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts.Where(x => x.NameOfPart.StartsWith(prefix)).ToList();
        }

        public List<CarParts> WhereNameStartsWithAndPriceIsGreaterThan(string prefix, decimal minPrice)
        {
            var carParts = _carPartsRepository.GetAll();
            return carParts.Where(x => x.NameOfPart.StartsWith(prefix) && x.Price > minPrice).ToList();
        }
    }
}

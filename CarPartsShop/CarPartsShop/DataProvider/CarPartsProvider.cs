using CarPartsShop.Entities;
using CarPartsShop.Repositories;

namespace CarPartsShop.DataProvider
{
    public class CarPartsProvider : ICarPartsProvider
    {
        private readonly IRepository<CarParts> _carPartsRepository;
        public CarPartsProvider(IRepository<CarParts> carPartsRepository) 
        {
            _carPartsRepository = carPartsRepository;
        }
        public List<CarParts> FilterCarParts(decimal minPrice)
        {
            var carParts = _carPartsRepository.GetAll();
            var list = new List<CarParts>();
            foreach (var carPart in carParts) 
            {
                if (carPart.Price > minPrice)
                {
                    list.Add(carPart);
                }
            }
            return list;
        }

        public decimal GetMinimalPriceOfCarParts()
        {
            var carParts = _carPartsRepository.GetAll();
            var minimum = decimal.MaxValue;
            foreach (var carPart in carParts)
            {
                if (carPart.Price < minimum)
                {
                    minimum = carPart.Price;
                }
            }
            return minimum;
        }

        public List<string> GetUniqueModelOfCars()
        {
            var carParts = _carPartsRepository.GetAll();
            var list = new List<string>();
            foreach (var carPart in carParts)
            {
                if (!list.Contains(carPart.ModelOfCar))
                {
                    list.Add(carPart.ModelOfCar);
                }
            }
            return list;
        }
    }
}

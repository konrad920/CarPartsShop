using CarPartsShop.Entities;

namespace CarPartsShop.Repositories
{
    public class CarPartsRepository
    {
        private readonly List<CarParts> _carparts = new();

        public void Add(CarParts item)
        {
            item.Id = _carparts.Count + 1;
            _carparts.Add(item);
        }

        public void Save()
        {
            foreach (var item in _carparts)
            {
                Console.WriteLine(item);
            }
        }
    }
}

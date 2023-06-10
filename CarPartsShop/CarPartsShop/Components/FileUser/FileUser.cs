using CarPartsShop.Components.CsvReader;
using CarPartsShop.Data;
using CarPartsShop.Data.Entities;

namespace CarPartsShop.Components.FileUser
{
    public class FileUser : IFileUser
    {
        private readonly CarPartsDBContext _carPartsDBContext;

        private readonly ICsvReader _csvReader;

        public FileUser(CarPartsDBContext carPartsDBContext, ICsvReader csvReader)
        {
            _carPartsDBContext = carPartsDBContext;
            _csvReader = csvReader;

        }
        public void CreateFile(string fileName)
        {
            var carPartsFromDB = _carPartsDBContext.CarParts.ToList();
            using (var writer = File.AppendText(fileName))
            {
                foreach (var carPart in carPartsFromDB)
                {
                    writer.WriteLine($"{carPart.Id}, {carPart.NameOfPart}, {carPart.IsUsed}, {carPart.Price}, {carPart.ModelOfCar}, {carPart.Sales}");
                }
            }
        }

        public void InsertDataFromFile(string fileName)
        {
            var carParts = _csvReader.CarPartsProcess(fileName);
            foreach (var carPart in carParts)
            {
                _carPartsDBContext.CarParts.Add(new CarParts
                {
                    NameOfPart = carPart.NameOfPart,
                    IsUsed = carPart.IsUsed,
                    Price = carPart.Price,
                    ModelOfCar = carPart.ModelOfCar,
                    Sales = carPart.Sales
                });
            }

            _carPartsDBContext.SaveChanges();
        }
    }
}

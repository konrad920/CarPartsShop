using CarPartsShop.Entities;
using CarPartsShop.Repositories;

var carRepository = new CarPartsRepository<CarParts>();
carRepository.Add(new CarParts { NameOfPart = "Silnik" });
carRepository.Add(new CarParts { NameOfPart = "Hamulce" });
carRepository.Add(new CarParts { NameOfPart = "Kierownica" });
carRepository.Add(new LorriesParts { NameOfPart = "Naczepa" });
carRepository.Add(new MotoParts { NameOfPart = "Lusterka" });


carRepository.Remove(carRepository.GetById(2));
var array = carRepository.GetAll();
foreach (var item in array)
{
    Console.WriteLine(item);
}
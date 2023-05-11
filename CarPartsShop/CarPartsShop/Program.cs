using CarPartsShop.Entities;
using CarPartsShop.Repositories;

var carRepository = new CarPartsRepository();
carRepository.Add(new CarParts { NameOfPart = "Silnik" });
carRepository.Add(new CarParts { NameOfPart = "Hamulce" });
carRepository.Add(new CarParts { NameOfPart = "Kierownica" });
carRepository.Save();
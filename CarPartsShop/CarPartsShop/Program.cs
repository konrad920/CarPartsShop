using CarPartsShop.Data;
using CarPartsShop.Entities;
using CarPartsShop.Repositories;

var carRepository = new SQLPartsRepository<CarParts>(new CarPartsDBContext());
AddNewLorryParts(carRepository);
AddNewMotorBikeParts(carRepository);
RemoveCarPartsById(carRepository);
WriteAllToConsole(carRepository);


static void AddNewCarParts(IRepository<CarParts>carRepository)
{
    carRepository.Add(new CarParts { NameOfPart = "Silnik" });
    carRepository.Add(new CarParts { NameOfPart = "Hamulce" });
    carRepository.Add(new CarParts { NameOfPart = "Kierownica" });
    carRepository.Save();
}

static void AddNewLorryParts(IWriteRepository<LorriesParts>lorryRepository)
{
    lorryRepository.Add(new LorriesParts { NameOfPart = "Naczepa"});
    lorryRepository.Add(new LorriesParts { NameOfPart = "Przyczepa" });
    lorryRepository.Save();
}

static void AddNewMotorBikeParts(IWriteRepository<MotoParts>motorBikeRepository)
{
    motorBikeRepository.Add(new MotoParts { NameOfPart = "Lusterka" });
    motorBikeRepository.Add(new MotoParts { NameOfPart = "Koło przednie" });
    motorBikeRepository.Save();
}

static void RemoveCarPartsById(IRepository<CarParts> Repository)
{
    Repository.Remove(Repository.GetById(2));
    Repository.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity>Repository)
{
    var _items = Repository.GetAll();
    foreach (var item in _items)
    {
        Console.WriteLine(item);
    }
}
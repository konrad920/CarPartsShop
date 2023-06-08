

using CarPartsShop;
using CarPartsShop.Components.CsvReader;
using CarPartsShop.Components.DataProvider;
using CarPartsShop.Components.UserCommunication;
using CarPartsShop.Data;
using CarPartsShop.Data.Entities;
using CarPartsShop.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Employee>, ListRepository<Employee>>();
services.AddSingleton<IRepository<CarParts>, ListRepository<CarParts>>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddDbContext<CarPartsDBContext>(options => options.UseSqlServer("Data Source=LAPTOP-QIGQKKJP\\SQLEXPRESS01;Initial Catalog=CarPartsStorage;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();



//using CarPartsShop.Data;
//using CarPartsShop.Entities;
//using CarPartsShop.Repositories;
//using CarPartsShop.Repositories.Extensions;

//Console.WriteLine("Welcome in my program CarPartsShop");
//Console.WriteLine("(A)Add new part");
//Console.WriteLine("(R)Remove a part");
//Console.WriteLine("(S)Show all parts");
//Console.WriteLine("(I)Show part by Id");
//Console.WriteLine("(Q)Quit the program");

//string eventListFile = "eventListFile.txt";
////var itemAdded = new Action<CarParts>(CarPartAdded);
//var carRepository = new SQLPartsRepository<CarParts>(new CarPartsDBContext());
//carrepository.itemadded += carpartrepositoryonitemadded;
//carrepository.itemremoved += carpartrepositoryonitemremoved;
//carrepository.filesavedadded += eventsavedtofileadded;
//carrepository.filesavedremoved += eventsavedtofileremoved;


//void EventSavedToFileRemoved(object sender, CarParts e)
//{
//    using (var writer = File.AppendText(eventListFile))
//    {
//        var data = DateTime.Now;
//        writer.WriteLine($"{data}, {e.NameOfPart}, from {sender.GetType().Name}, removed");
//    }
//}
//void EventSavedToFileAdded(object sender, CarParts e)
//{
//    using (var writer = File.AppendText(eventListFile))
//    {
//        var data = DateTime.Now;
//        writer.WriteLine($"{data}, {e.NameOfPart}, from {sender.GetType().Name}, added");
//    }
//}
//static void CarPartRepositoryOnItemAdded(object sender, CarParts e)
//{
//    Console.WriteLine($"Car Parts added: {e.NameOfPart}, from {sender.GetType().Name}");
//}

//static void CarPartRepositoryOnItemRemoved(object sender, CarParts e)
//{
//    Console.WriteLine($"Car Parts removed: {e.NameOfPart}, from {sender.GetType().Name}");
//}

//while (true)
//{
//    Console.Write("Chose what do you want: ");
//    var userAnswer = Console.ReadLine();
//    if (userAnswer == "a" || userAnswer == "A")
//    {
//        var carParts = new CarParts();
//        Console.Write("Name of new part: ");
//        var itemToAdd = Console.ReadLine();
//        carParts.NameOfPart = itemToAdd;
//        carRepository.Add(carParts);
//    }
//    else if (userAnswer == "r" || userAnswer == "R")
//    {
//        try
//        {
//            Console.Write("Which part you want remove, enter Id: ");
//            var idToRemove = Console.ReadLine();
//            var carPartsToRemove = new CarParts();
//            carPartsToRemove.Id = int.Parse(idToRemove);
//            carRepository.Remove1(carPartsToRemove);
//            //carRepository.Remove(carRepository.GetById(int.Parse(idToRemove)));
//            //carRepository.Save();
//        }
//        catch(Exception ex) 
//        { 
//            Console.WriteLine($"Exception catched: {ex.Message}"); 
//        }
//    }
//    else if (userAnswer == "s" || userAnswer == "S")
//    {
//        try
//        {
//            var _items = carRepository.GetAll1();
//            foreach (var item in _items)
//            {
//                Console.WriteLine(item);
//            }
//        }
//        catch(Exception ex)
//        {
//            Console.WriteLine($"Exception catched: {ex.Message}");
//        }
//    }
//    else if (userAnswer == "i" || userAnswer == "I")
//    {
//        try
//        {
//            Console.Write("Which part you want see, enter Id: ");
//            var idToShow = Console.ReadLine();
//            var carPartToShow = carRepository.GetById1(int.Parse(idToShow));
//            Console.WriteLine(carPartToShow);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Exception catched: {ex.Message}");
//        }
//    }
//    else if (userAnswer == "q" || userAnswer == "Q")
//    {
//        Console.WriteLine("You close the program");
//        break;
//    }
//}



//AddNewCarParts(carRepository);
//RemoveCarPartsById(carRepository);
//WriteAllToConsole(carRepository);

//static void CarPartAdded(CarParts item)
//{
//    Console.WriteLine($"{item.NameOfPart}, added");
//}
//static void AddNewCarParts(IRepository<CarParts>carRepository)
//{
//    var carParts = new[]
//    {
//        new CarParts { NameOfPart = "Silnik" },
//        new CarParts { NameOfPart = "Hamulce" },
//        new CarParts { NameOfPart = "Kierownica" }
//    };

//    carRepository.AddBatch(carParts);
//    //AddBatch(carRepository, carParts);
//}


//static void RemoveCarPartsById(IRepository<CarParts> Repository)
//{
//    Repository.Remove(Repository.GetById(2));
//    Repository.Save();
//}

//static void WriteAllToConsole(IReadRepository<IEntity>Repository)
//{
//    var _items = Repository.GetAll();
//    foreach (var item in _items)
//    {
//        Console.WriteLine(item);
//    }
//}
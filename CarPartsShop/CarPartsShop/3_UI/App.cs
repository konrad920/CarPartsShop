using CarPartsShop.Components.CsvReader;
using CarPartsShop.Components.FileUser;
using CarPartsShop.Components.UserCommunication;
using CarPartsShop.Data;
using CarPartsShop.Data.Entities;
using CarPartsShop.Data.Repositories;
using System.Xml.Linq;

namespace CarPartsShop
{
    public class App : IApp
    {
        private readonly string DBFileName = "StorageFromDB.csv";

        private readonly string eventsRaport = "EventsRaport.txt";

        private readonly IRepository<Employee> _employeeRepository;

        private readonly IRepository<CarParts> _carPartsRepository;

        private readonly IUserCommunication _userCommunication;

        private readonly CarPartsDBContext _carPartsDBContext;

        private readonly IFileUser _fileUser;

        public App(IRepository<Employee> employeeRepository,
                   IRepository<CarParts> carPartsRepository,
                   IUserCommunication userCommunication,
                   CarPartsDBContext carPartsDBContext,
                   IFileUser fileUser)
        {
            _employeeRepository = employeeRepository;
            _carPartsRepository = carPartsRepository;
            _userCommunication = userCommunication;
            _fileUser = fileUser;
            _carPartsDBContext = carPartsDBContext;
            _carPartsDBContext.Database.EnsureCreated();
        }
        public void Run()
        {
            _carPartsRepository.ItemAdded += CarPartRepositoryOnItemAdded;
            _carPartsRepository.ItemRemoved += CarPartRepositoryOnItemRemoved;
            _carPartsRepository.FileSavedAdded += CarPartRepositoryFileSaveAdded;
            _carPartsRepository.FileSavedRemoved += CarPartRepositoryFileSaveRemoved;

            while (true)
            {
                Console.Write(_userCommunication.BeginProgram());
                var userChoose = _userCommunication.UserChoose();
                if (userChoose == "q" || userChoose == "Q")
                {
                    break;
                }
                else if (userChoose == "a" || userChoose == "A")
                {
                    try
                    {
                        var carPart = _userCommunication.CreateNewCarPart();
                        _carPartsRepository.Add(carPart);
                        _carPartsRepository.Save();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Exception catched: {ex.Message}");
                    }
                }
                else if (userChoose == "r" || userChoose == "R")
                {
                    try
                    {
                        var partToRemove = _userCommunication.GetPartById();
                        _userCommunication.RemovePart(partToRemove);
                        _carPartsRepository.Save();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Exception catched: {ex.Message}");
                    }
                }
                else if (userChoose == "s" || userChoose == "S")
                {
                    try
                    {
                        _userCommunication.GetAllPart();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception catched: {ex.Message}");
                    }
                }
                else if (userChoose == "i" || userChoose == "I")
                {
                    try
                    {
                        _userCommunication.GetPartById();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception catched: {ex.Message}");
                    }
                }
                else if (userChoose =="e" || userChoose == "E")
                {
                    try
                    {
                        var partToEdit = _userCommunication.GetPartById();
                        _userCommunication.EditPart(partToEdit);
                        _carPartsRepository.Save();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception catched: {ex.Message}");
                    }
                }
                else if (userChoose == "c" || userChoose == "C")
                {
                    _fileUser.CreateFile(DBFileName);
                }
                else if (userChoose == "in" || userChoose == "In")
                {
                    _fileUser.InsertDataFromFile(DBFileName);
                }
                else if (userChoose == "g" || userChoose == "G")
                {
                    _userCommunication.GroupedData();
                }
                else
                {
                    Console.WriteLine("Wrong char, please try again");
                    continue;
                }
            }
        }

        private void _carPartsRepository_FileSavedAdded(object? sender, CarParts e)
        {
            throw new NotImplementedException();
        }

        public static void CarPartRepositoryOnItemAdded(object sender, CarParts e)
        {
            Console.WriteLine($"Car Parts added: {e.NameOfPart}, from {sender.GetType().Name}");
        }

        public static void CarPartRepositoryOnItemRemoved(object sender, CarParts e)
        {
            Console.WriteLine($"Car Parts removed: {e.NameOfPart}, from {sender.GetType().Name}");
        }

        public void CarPartRepositoryFileSaveAdded(object sender, CarParts e)
        {
            using(var writer = File.AppendText(eventsRaport))
            {
                writer.WriteLine($"{DateTime.Now}, Carpart name: {e.NameOfPart}, from {sender.GetType().Name}, added");
            }
        }

        public void CarPartRepositoryFileSaveRemoved(object sender, CarParts e)
        {
            using (var writer = File.AppendText(eventsRaport))
            {
                writer.WriteLine($"{DateTime.Now}, Carpart name: {e.NameOfPart}, from {sender.GetType().Name}, removed");
            }
        }
    }
}

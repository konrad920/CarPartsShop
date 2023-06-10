using CarPartsShop.Components.CsvReader;
using CarPartsShop.Components.FileUser;
using CarPartsShop.Components.UserCommunication;
using CarPartsShop.Data;
using CarPartsShop.Data.Entities;
using CarPartsShop.Data.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarPartsShop
{
    public class App : IApp
    {
        private readonly string fileName = "StorageFromDB.csv";

        private readonly IRepository<Employee> _employeeRepository;

        private readonly IRepository<CarParts> _carPartsRepository;

        private readonly IUserCommunication _userCommunication;

        private readonly ICsvReader _csvReader;

        private readonly CarPartsDBContext _carPartsDBContext;

        private readonly IFileUser _fileUser;

        public App(IRepository<Employee> employeeRepository,
                   IRepository<CarParts> carPartsRepository,
                   IUserCommunication userCommunication,
                   ICsvReader csvReader,
                   CarPartsDBContext carPartsDBContext,
                   IFileUser fileUser)
        {
            _employeeRepository = employeeRepository;
            _carPartsRepository = carPartsRepository;
            _userCommunication = userCommunication;
            _csvReader = csvReader;
            _fileUser = fileUser;
            _carPartsDBContext = carPartsDBContext;
            _carPartsDBContext.Database.EnsureCreated();
        }
        public void Run()
        {
            //InsertData();
            //ReadData();
            //ReadGroupedCarFromDB();

            //edycja danych w bazie danych
            //var cayman = this.ReadFirst("Cayman");
            //cayman.Name = "nowySamochod";
            //_carPartsDBContext.SaveChanges();


            //usuwanie z bazy danych
            //var carToRemove = this.ReadFirst("nowySamochod");
            //_carPartsDBContext.CarParts.Remove(carToRemove);
            //_carPartsDBContext.SaveChanges();

            var repo = new ListRepository<CarParts>();
            repo.ItemAdded += CarPartRepositoryOnItemAdded;
            repo.ItemRemoved += CarPartRepositoryOnItemRemoved;

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
                    var carPart = _userCommunication.AddNewCarPart();
                    _carPartsRepository.Add(carPart);
                    _carPartsRepository.Save();
                    repo.Add(carPart);
                }
                else if (userChoose == "r" || userChoose == "R")
                {
                    var carPart = _userCommunication.RemovePartId();
                    _carPartsRepository.Remove(carPart);
                    _carPartsRepository.Save();
                    repo.Remove(carPart);
                }
                else if (userChoose == "s" || userChoose == "S")
                {
                    _userCommunication.GetAllPart();
                }
                else if (userChoose == "i" || userChoose == "I")
                {
                    _userCommunication.ShowPartById();
                }
                else if (userChoose =="e" || userChoose == "E")
                {
                    var carPart = _userCommunication.GetPartByIDToEdit();
                    _userCommunication.EditPart(carPart);
                    _carPartsRepository.Save();
                }
                else if (userChoose == "c" || userChoose == "C")
                {
                    _fileUser.CreateFile(fileName);
                }
                else if (userChoose == "in" || userChoose == "In")
                {
                    _fileUser.InsertDataFromFile(fileName);
                }
                else if (userChoose == "g" || userChoose == "G")
                {
                    var groups = _carPartsDBContext.CarParts
                        .GroupBy(x => x.ModelOfCar)
                        .Select(g => new
                        {
                            Name = g.Key,
                            CarParts = g.Select(c => new {c.Sales, c.NameOfPart, c.Price})
                        }).ToList();
                    foreach (var group in groups)
                    {
                        Console.WriteLine(group.Name);
                        Console.WriteLine("=========");
                        foreach (var part in group.CarParts)
                        {
                            Console.WriteLine($"{part.NameOfPart}, sales {part.Sales}, each cost {part.Price}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Wrong char, please try again");
                    continue;
                }
            }
        }
        private CarParts? ReadFirst(string name)
        {
            return _carPartsDBContext.CarParts.FirstOrDefault(x => x.NameOfPart == name);
        }

        public static void CarPartRepositoryOnItemAdded(object sender, CarParts e)
        {
            Console.WriteLine($"Car Parts added: {e.NameOfPart}, from {sender.GetType().Name}");
        }

        public static void CarPartRepositoryOnItemRemoved(object sender, CarParts e)
        {
            Console.WriteLine($"Car Parts removed: {e.NameOfPart}, from {sender.GetType().Name}");
        }
    }
}

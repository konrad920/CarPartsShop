using CarPartsShop.Components.CsvReader;
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

        public App(IRepository<Employee> employeeRepository,
                   IRepository<CarParts> carPartsRepository,
                   IUserCommunication userCommunication,
                   ICsvReader csvReader,
                   CarPartsDBContext carPartsDBContext)
        {
            _employeeRepository = employeeRepository;
            _carPartsRepository = carPartsRepository;
            _userCommunication = userCommunication;
            _csvReader = csvReader;
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
                    var carPartsFromDB = _carPartsDBContext.CarParts.ToList();
                    using (var writer = File.AppendText(fileName))
                    {
                        foreach (var carPart in carPartsFromDB)
                        {
                            writer.WriteLine($"{carPart.Id}, {carPart.NameOfPart}, {carPart.IsUsed}, {carPart.Price}, {carPart.ModelOfCar}, {carPart.Sales}");
                        }
                    }
                }
                else if (userChoose == "in" || userChoose == "In")
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


        //private void ReadGroupedCarFromDB()
        //{
        //    var groups = _carPartsDBContext.CarParts
        //        .GroupBy(x => x.Manufacturer)
        //        .Select(x => new
        //        {
        //            Name = x.Key,
        //            Car = x.ToList()
        //        })
        //        .ToList();

        //    foreach(var car in groups)
        //    {
        //        Console.WriteLine($"Name: {car.Name}");
        //        Console.WriteLine("=========");
        //        foreach(var item in car.Car)
        //        {
        //            Console.WriteLine($"\tName:{item.Name}, Combined: {item.Combined}");
        //        }
        //    }
        //}

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

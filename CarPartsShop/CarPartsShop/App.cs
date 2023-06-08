using CarPartsShop.Components.CsvReader;
using CarPartsShop.Components.UserCommunication;
using CarPartsShop.Data;
using CarPartsShop.Data.Entities;
using CarPartsShop.Data.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            var cayman = this.ReadFirst("Cayman");
            cayman.Name = "nowySamochod";
            _carPartsDBContext.SaveChanges();


            //usuwanie z bazy danych
            var carToRemove = this.ReadFirst("nowySamochod");
            _carPartsDBContext.CarParts.Remove(carToRemove);
            _carPartsDBContext.SaveChanges();

            //var repo = new ListRepository<CarParts>();
            //repo.ItemAdded += CarPartRepositoryOnItemAdded;
            //repo.ItemRemoved += CarPartRepositoryOnItemRemoved;

            //while (true)
            //{
            //    Console.Write(_userCommunication.BeginProgram());
            //    var userChoose = _userCommunication.UserChoose();
            //    if (userChoose == "q" || userChoose == "Q")
            //    {
            //        break;
            //    }
            //    else if (userChoose == "a" || userChoose == "A")
            //    {
            //        var carPart = _userCommunication.AddNewCarPart();
            //        _carPartsRepository.Add(carPart);
            //        repo.Add(carPart);
            //    }
            //    else if (userChoose == "r" || userChoose == "R")
            //    {
            //        var carPart = _userCommunication.RemovePartId();
            //        _carPartsRepository.Remove(carPart);
            //        repo.Remove(carPart);
            //    }
            //    else if (userChoose == "s" || userChoose == "S")
            //    {
            //        _userCommunication.GetAllPart();
            //    }
            //    else if (userChoose == "i" || userChoose == "I")
            //    {
            //        _userCommunication.GetPartById();
            //    }
            //    else
            //    {
            //        Console.WriteLine("Wrong char, please try again");
            //        continue;
            //    }
            //}
        }
        private Car? ReadFirst(string name)
        {
            return _carPartsDBContext.CarParts.FirstOrDefault(x => x.Name == name);
        }
        private void InsertData()
        {
            var cars = _csvReader.CarProcess("Resources\\Files\\fuel.csv");
            foreach (var car in cars)
            {
                _carPartsDBContext.CarParts.Add(new Car
                {
                    Year = car.Year,
                    Manufacturer = car.Manufacturer,
                    Name = car.Name,
                    Displacement = car.Displacement,
                    Cylinders = car.Cylinders,
                    City = car.City,
                    Highway = car.Highway,
                    Combined = car.Combined
                });
            }

            _carPartsDBContext.SaveChanges();
        }

        private void ReadData()
        {
            var carPartsFromDB = _carPartsDBContext.CarParts.ToList();

            foreach (var carPart in carPartsFromDB)
            {
                Console.WriteLine($"\t{carPart.Name}, {carPart.Combined}");
            }
        }

        private void ReadGroupedCarFromDB()
        {
            var groups = _carPartsDBContext.CarParts
                .GroupBy(x => x.Manufacturer)
                .Select(x => new
                {
                    Name = x.Key,
                    Car = x.ToList()
                })
                .ToList();

            foreach(var car in groups)
            {
                Console.WriteLine($"Name: {car.Name}");
                Console.WriteLine("=========");
                foreach(var item in car.Car)
                {
                    Console.WriteLine($"\tName:{item.Name}, Combined: {item.Combined}");
                }
            }
        }

        //public static void CarPartRepositoryOnItemAdded(object sender, CarParts e)
        //{
        //    Console.WriteLine($"Car Parts added: {e.NameOfPart}, from {sender.GetType().Name}");
        //}

        //public static void CarPartRepositoryOnItemRemoved(object sender, CarParts e)
        //{
        //    Console.WriteLine($"Car Parts removed: {e.NameOfPart}, from {sender.GetType().Name}");
        //}
    }
}

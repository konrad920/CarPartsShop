using CarPartsShop.Components.CsvReader;
using CarPartsShop.Components.CsvReader.Models;
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
            var repo = new MSQLRepository<CarParts>(_carPartsDBContext);
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
                    var carPart = _userCommunication.CreateNewCarPart();
                    _carPartsRepository.Add(carPart);
                    repo.Add(carPart);
                    _carPartsRepository.Save();
                }
                else if (userChoose == "r" || userChoose == "R")
                {
                    var partToRemove = _userCommunication.GetPartById();
                    _userCommunication.RemovePart(partToRemove);
                    repo.Remove(partToRemove);
                    _carPartsRepository.Save();
                }
                else if (userChoose == "s" || userChoose == "S")
                {
                    _userCommunication.GetAllPart();
                }
                else if (userChoose == "i" || userChoose == "I")
                {
                    _userCommunication.GetPartById();
                }
                else if (userChoose =="e" || userChoose == "E")
                {
                    var partToEdit = _userCommunication.GetPartById();
                    _userCommunication.EditPart(partToEdit);
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
                    _userCommunication.GroupedData();
                }
                else
                {
                    Console.WriteLine("Wrong char, please try again");
                    continue;
                }
            }
        }
        public static void CarPartRepositoryOnItemAdded(object sender, CarParts e)
        {
            Console.WriteLine($"Car Parts added: {e.NameOfPart}, from {sender.GetType().Name}");
        }

        public static void CarPartRepositoryOnItemRemoved(object sender, CarParts e)
        {
            Console.WriteLine($"Car Parts removed: {e.NameOfPart}, from {sender.GetType().Name}");
        }

        //public void GetTotalSales()
        //{
        //    var groups = _carPartsDBContext.CarParts.GroupBy(x => x.ModelOfCar);
        //    foreach ( var group in groups)
        //    {
        //        var carPart = group.GroupBy(x => x.NameOfPart).Select(g => new
        //        {
        //            Name = g.Key,
        //            Sales = g.Select(x => x.Sales.Value),
        //        });
        //        int total = 0;
        //        foreach ( var part in carPart)
        //        {
        //            total += part.Sales.Sum();
        //        }
        //        Console.WriteLine($"Model {group}");
        //    }
            
        //}
    }
}

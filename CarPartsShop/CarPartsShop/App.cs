using CarPartsShop.Components.CsvReader;
using CarPartsShop.Components.CsvReader.Models;
using CarPartsShop.Components.DataProvider;
using CarPartsShop.Components.UserCommunication;
using CarPartsShop.Data.Entities;
using CarPartsShop.Data.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        private readonly ICarPartsProvider _carPartsProvider;

        private readonly ICsvReader _csvReader;

        public App(
            IRepository<Employee> employeeRepository,
            IRepository<CarParts> carPartsRepository,
            IUserCommunication userCommunication,
            ICarPartsProvider carPartsProvider,
            ICsvReader csvReader)
        {
            _employeeRepository = employeeRepository;
            _carPartsRepository = carPartsRepository;
            _userCommunication = userCommunication;
            _carPartsProvider = carPartsProvider;
            _csvReader = csvReader;
        }
        public void Run()
        {
            //var cars = _csvReader.CarProcess("Resources\\Files\\fuel.csv");
            //var manufactures = _csvReader.ManufacturerProcess("Resources\\Files\\manufacturers.csv");

            //var groups = cars.GroupBy(x => x.Manufacturer)
            //    .Select(g => new
            //    {
            //        Name = g.Key,
            //        Max = g.Max(c => c.Combined),
            //        Averange = g.Average(c => c.Combined),

            //    })
            //    .OrderBy(x => x.Averange)
            //    .ThenBy(x => x.Name);


            //var groups1 = cars.Join(
            //    manufactures,
            //    c => c.Manufacturer,
            //    m => m.Name,
            //    (car, manufactur) => new
            //    {
            //        manufactur.Name,
            //        car.City,
            //        car.Combined
            //    })
            //    .OrderByDescending(x => x.Combined)
            //    .ThenBy(x => x.Name);

            //var groups2 = cars.GroupJoin(
            //    manufactures,
            //    c => c.Manufacturer,
            //    m => m.Name,
            //    (c, m) => new
            //    {
            //        Cars = c,
            //        Manufactors = m
            //    })
            //    .OrderBy(x => x.Manufactors);

            //CreateXML();
            //ReadXML();

            //void CreateXML()
            //{
            //    var cars = _csvReader.CarProcess("Resources\\Files\\fuel.csv");
            //    var document = new XDocument();
            //    var records = new XElement("Cars", cars
            //    .Select(x =>
            //        new XElement("Car",
            //            new XAttribute("Name", x.Name),
            //            new XAttribute("Combined", x.Combined),
            //            new XAttribute("Manufacturer", x.Manufacturer)
            //            )
            //    ));

            //    document.Add(records);
            //    document.Save("fuel.xml");
            //}


            //static void ReadXML()
            //{
            //    var document = XDocument.Load("fuel.xml");
            //    var names = document
            //        .Elements("Cars")
            //        .Elements("Car")
            //        .Where(x => x.Attribute("Manufacturer")?.Value == "BMW")
            //        .Select(x => x.Attribute("Name")?.Value);

            //    foreach (var name in names)
            //    {
            //        Console.WriteLine(name);
            //    }
            //}

            //foreach (var group in groups)
            //{
            //    Console.WriteLine(group.Name);
            //    Console.WriteLine($" \t Max: {group.Max}");
            //    Console.WriteLine($" \t Averange: {group.Averange}");
            //}

            //foreach (var group in groups1)
            //{
            //    Console.WriteLine(group.Name);
            //    Console.WriteLine($" \t City: {group.City}");
            //    Console.WriteLine($" \t Combined: {group.Combined}");
            //}

            //zadanie domowe
            void CreateXMLWithStencil()
            {
                var cars = _csvReader.CarProcess("Resources\\Files\\fuel.csv");
                var manufacturers = _csvReader.ManufacturerProcess("Resources\\Files\\manufacturers.csv");

                var carsNeeded = cars.Join(manufacturers,
                    c => c.Manufacturer,
                    m => m.Name,
                    (car, manufacture) => new
                    {
                        NameGroupOfCars = manufacture.Name,
                        Country = manufacture.Country,
                        ModelOfCar = car.Name,
                        Combined = car.Combined,
                    });

                var carsGrouped = carsNeeded.GroupBy(x => x.NameGroupOfCars)
                    .Select(g => new
                    {
                        Name = g.Key,
                        Sum = g.Sum(c => c.Combined),
                        Country = g.First().Country,
                        ModelOfCar = g.Select(c => new { c.ModelOfCar, c.Combined}),
                    });

                var document = new XDocument();

                var records = new XElement("Manufacturers", carsGrouped
                    .Select(x =>
                        new XElement("Manufacture",
                            new XAttribute("Name", x.Name),
                            new XAttribute("Country", x.Country),
                            new XElement("Cars",
                                new XAttribute("country", x.Country),
                                new XAttribute("CombinedSum", x.Sum),
                                x.ModelOfCar.Select(m=>
                                new XElement("Car",
                                    new XAttribute("Model", m.ModelOfCar),
                                    new XAttribute("Combined", m.Combined)
                                    )
                                ))
                            )
                    ));
                document.Add( records );
                document.Save("raport.xml");
            }

            CreateXMLWithStencil();




            //var repo = new ListRepository<CarParts>();
            //repo.ItemAdded += CarPartRepositoryOnItemAdded;
            //repo.ItemRemoved += CarPartRepositoryOnItemRemoved;

            //    while (true)
            //    {
            //        Console.Write(_userCommunication.BeginProgram());
            //        var userChoose = _userCommunication.UserChoose();
            //        if (userChoose == "q" || userChoose == "Q")
            //        {
            //            break;
            //        }
            //        else if (userChoose == "a" || userChoose == "A")
            //        {
            //            var carPart = _userCommunication.AddNewCarPart();
            //            _carPartsRepository.Add(carPart);
            //            repo.Add(carPart);
            //        }
            //        else if (userChoose == "r" || userChoose == "R")
            //        {
            //            var carPart = _userCommunication.RemovePartId();
            //            _carPartsRepository.Remove(carPart);
            //            repo.Remove(carPart);
            //        }
            //        else if (userChoose == "s" || userChoose == "S")
            //        {
            //            _userCommunication.GetAllPart();
            //        }
            //        else if (userChoose == "i" || userChoose == "I")
            //        {
            //            _userCommunication.GetPartById();
            //        }
            //        else
            //        {
            //            Console.WriteLine("Wrong char, please try again");
            //            continue;
            //        }
            //    }
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

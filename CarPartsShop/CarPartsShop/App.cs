using CarPartsShop.DataProvider;
using CarPartsShop.Entities;
using CarPartsShop.Repositories;
using CarPartsShop.UserCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarPartsShop
{
    public class App : IApp
    {
        private readonly IRepository<Employee> _employeeRepository;

        private readonly IRepository<CarParts> _carPartsRepository;

        private readonly IUserCommunication _userCommunication;

        private readonly ICarPartsProvider _carPartsProvider;

        public App(
            IRepository<Employee> employeeRepository, 
            IRepository<CarParts> carPartsRepository,
            IUserCommunication userCommunication,
            ICarPartsProvider carPartsProvider)
        {
            _employeeRepository = employeeRepository;
            _carPartsRepository = carPartsRepository;
            _userCommunication = userCommunication;
            _carPartsProvider = carPartsProvider;
        }
        public void Run()
        {
            
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
                    _userCommunication.AddNewCarPart();
                }
                else if (userChoose == "r" || userChoose == "R")
                {
                    _userCommunication.RemovePartId();
                }
                else if (userChoose == "s" || userChoose == "S")
                {
                    _userCommunication.GetAllPart();
                }
                else if (userChoose == "i" || userChoose == "I")
                {
                    _userCommunication.GetPartById();
                }
                else
                {
                    Console.WriteLine("Wrong char, please try again");
                    continue;
                }
            }
        }

        //public static List<CarParts> GenerateSamplesCarParts()
        //{
        //    return new List<CarParts>
        //    {
        //        new CarParts 
        //        {
        //            Id = 1,
        //            NameOfPart = "Part1",
        //            IsUsed = "new",
        //            ModelOfCar = "Car 1",
        //            Price = 340.21M
        //        },
        //        new CarParts
        //        {
        //            Id = 10,
        //            NameOfPart = "Part2",
        //            IsUsed = "new",
        //            ModelOfCar = "Car 2",
        //            Price = 142.53M
        //        },
        //        new CarParts
        //        {
        //            Id = 2,
        //            NameOfPart = "Part3",
        //            IsUsed = "used",
        //            ModelOfCar = "Car 1",
        //            Price = 15.81M
        //        },
        //        new CarParts
        //        {
        //            Id = 14,
        //            NameOfPart = "Part1",
        //            IsUsed = "used",
        //            ModelOfCar = "Car 2",
        //            Price = 152.74M
        //        },
        //        new CarParts
        //        {
        //            Id = 4,
        //            NameOfPart = "Part4",
        //            IsUsed = "new",
        //            ModelOfCar = "Car 3",
        //            Price = 1236.54M
        //        },
        //        new CarParts
        //        {
        //            Id = 21,
        //            NameOfPart = "Part5",
        //            IsUsed = "new",
        //            ModelOfCar = "Car 4",
        //            Price = 456.18M
        //        },
        //        new CarParts
        //        {
        //            Id = 22,
        //            NameOfPart = "Part6",
        //            IsUsed = "new",
        //            ModelOfCar = "Car 5",
        //            Price = 198.17M
        //        },
        //        new CarParts
        //        {
        //            Id = 25,
        //            NameOfPart = "Part7",
        //            IsUsed = "used",
        //            ModelOfCar = "Car 4",
        //            Price = 357.86M
        //        },
        //        new CarParts
        //        {
        //            Id = 32,
        //            NameOfPart = "Part8",
        //            IsUsed = "new",
        //            ModelOfCar = "Car 4",
        //            Price = 1357.86M
        //        },
        //        new CarParts
        //        {
        //            Id = 36,
        //            NameOfPart = "Part8",
        //            IsUsed = "new",
        //            ModelOfCar = "Car 5",
        //            Price = 1627.86M
        //        },
        //        new CarParts
        //        {
        //            Id = 37,
        //            NameOfPart = "Part8",
        //            IsUsed = "used",
        //            ModelOfCar = "Car 2",
        //            Price = 845.69M
        //        },
        //        new CarParts
        //        {
        //            Id = 42,
        //            NameOfPart = "Part9",
        //            IsUsed = "new",
        //            ModelOfCar = "Car 1",
        //            Price = 98.15M
        //        },
        //        new CarParts
        //        {
        //            Id = 44,
        //            NameOfPart = "Part9",
        //            IsUsed = "new",
        //            ModelOfCar = "Car 3",
        //            Price = 59.15M
        //        },
        //        new CarParts
        //        {
        //            Id = 7,
        //            NameOfPart = "Part10",
        //            IsUsed = "used",
        //            ModelOfCar = "Car 6",
        //            Price = 485.16M
        //        }
        //    };
        //}
    }
}

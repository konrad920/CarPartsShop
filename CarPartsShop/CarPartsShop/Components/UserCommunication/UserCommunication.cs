﻿using CarPartsShop.Data.Entities;
using CarPartsShop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Components.UserCommunication
{
    public class UserCommunication : IUserCommunication
    {
        private readonly IRepository<CarParts> _carPartsRepository;

        public UserCommunication(IRepository<CarParts> carPartsRepository)
        {
            _carPartsRepository = carPartsRepository;
        }

        public string BeginProgram()
        {
            var sb = new StringBuilder();
            sb.AppendLine("This is my CarPartsShop program");
            sb.AppendLine("(A) - Add a new part");
            sb.AppendLine("(R) - Remove the exist part");
            sb.AppendLine("(S) - Show all of parts");
            sb.AppendLine("(I) - Show part by Id");
            sb.Append("Select what do you want: ");

            return sb.ToString();
        }

        public string UserChoose()
        {
            var userChoose = Console.ReadLine();
            Console.WriteLine();
            return userChoose;
        }

        public CarParts AddNewCarPart()
        {
            var carPart = new CarParts();
            Console.Write("Name of CarPart: ");
            var nameOfCarPart = Console.ReadLine();
            Console.Write("Car model name: ");
            var nameOfCarModel = Console.ReadLine();
            Console.Write("Is used or new: ");
            var isUsed = Console.ReadLine();
            Console.Write("Price for this part: ");
            var price = Console.ReadLine();
            if (decimal.TryParse(price, out var result))
            {
                carPart.Price = result;
            }
            else
            {
                Console.WriteLine("Wrong format of data");
            }
            carPart.NameOfPart = nameOfCarPart;
            carPart.ModelOfCar = nameOfCarModel;
            carPart.IsUsed = isUsed;
            return carPart;
            //_carPartsRepository.Add(carPart);
        }

        public CarParts RemovePartId()
        {
            Console.Write("You want remove part at Id: ");
            var IdToRemove = Console.ReadLine();
            if (int.TryParse(IdToRemove, out var result))
            {
                var itemToRemove = _carPartsRepository.GetById(result);
                if (itemToRemove != null)
                {
                    return itemToRemove;
                    //_carPartsRepository.Remove(itemToRemove);
                }
                else
                {
                    Console.WriteLine("Id is not exist");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("This Id is not integer!");
                return null;
            }
        }

        public void GetPartById()
        {
            Console.Write("You want see part at Id: ");
            var IdToShow = Console.ReadLine();
            if (int.TryParse(IdToShow, out var result))
            {
                var itemToShow = _carPartsRepository.GetById(result);
                if (itemToShow != null)
                {
                    Console.WriteLine(itemToShow.ToString());
                }
                else
                {
                    Console.WriteLine("Id is not exist");
                }
            }
            else
            {
                Console.WriteLine("This Id is not integer!");
            }
        }

        public void GetAllPart()
        {
            var list = _carPartsRepository.GetAll();
            if (list.Count() != 0)
            {
                foreach (var item in list)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else
            {
                Console.WriteLine("The base is empty");
            }
        }
    }
}
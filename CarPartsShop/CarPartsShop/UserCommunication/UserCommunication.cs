using CarPartsShop.Entities;
using CarPartsShop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.UserCommunication
{
    public class UserCommunication : IUserCommunication
    {
        private readonly IRepository<CarParts> _carPartsRepository;

        public UserCommunication(IRepository<CarParts> carPartsRepository)
        {
            _carPartsRepository = carPartsRepository;
        }

        public static void CarPartRepositoryOnItemAdded(object sender, CarParts e)
        {
            Console.WriteLine($"Car Parts added: {e.NameOfPart}, from {sender.GetType().Name}");
        }

        public static void CarPartRepositoryOnItemRemoved(object sender, CarParts e)
        {
            Console.WriteLine($"Car Parts removed: {e.NameOfPart}, from {sender.GetType().Name}");
        }

        private void cos()
        {
            var item = new ListRepository<CarParts>();
            item.ItemAdded += CarPartRepositoryOnItemAdded;
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

        public void AddNewCarPart()
        {
            var carPart = new CarParts();
            Console.Write("Name of CarPart: ");
            var nameOfCarPart = Console.ReadLine();
            Console.Write("Car model name: ");
            var nameOfCarModel = Console.ReadLine();
            Console.Write("Is used or new: ");
            var isUsed = Console.ReadLine();
            Console.Write("Price for this part: ");
            var price = decimal.Parse(Console.ReadLine());

            carPart.Price = price;
            carPart.NameOfPart = nameOfCarPart;
            carPart.ModelOfCar = nameOfCarModel;
            carPart.IsUsed = isUsed;
            _carPartsRepository.Add(carPart);
        }

            public void RemovePartId()
        {
            Console.Write("You want remove part at Id: ");
            var IdToRemove = Console.ReadLine();
            if (int.TryParse(IdToRemove, out var result))
            {
                var itemToRemove = _carPartsRepository.GetById(result);
                if (itemToRemove != null)
                {
                    _carPartsRepository.Remove(itemToRemove);
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

using CarPartsShop.Data.Entities;
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
            sb.AppendLine("(E) - Edit part by Id");
            sb.AppendLine("(C) - Create new file from DB");
            sb.AppendLine("(In) - Insert data from file to DB");
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
            Console.Write("Sales of this part: ");
            var sales = Console.ReadLine();
            if (int.TryParse(sales, out var result1))
            {
                carPart.Sales = result1;
            }
            else
            {
                Console.WriteLine("Wrong format of data");
            }
            carPart.NameOfPart = nameOfCarPart;
            carPart.ModelOfCar = nameOfCarModel;
            carPart.IsUsed = isUsed;
            return carPart;
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

        public CarParts GetPartByIDToEdit()
        {
            Console.Write("You want edit part at Id: ");
            var IdToEdit = Console.ReadLine();
            if (int.TryParse(IdToEdit, out var result))
            {
                var itemToEdit = _carPartsRepository.GetById(result);
                if (itemToEdit != null)
                {
                    return itemToEdit;
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

        public void EditPart(CarParts item)
        {
            Console.WriteLine("Which property you want to edit :");
            Console.WriteLine("[N] - Name of the part");
            Console.WriteLine("[M] - Model of the car");
            Console.WriteLine("[S] - Sales of the part");
            Console.WriteLine("[P] - Price of the part");
            Console.WriteLine("[I] - Is used of the part");
            var editChoose = Console.ReadLine();

            if (editChoose == "n" || editChoose == "N")
            {
                Console.Write("Write new carpart name: ");
                var editedName = Console.ReadLine();
                item.NameOfPart = editedName;
            }
            else if(editChoose == "m" || editChoose == "M")
            {
                Console.Write("Write new car model matched with this part: ");
                var editedModel = Console.ReadLine();
                item.ModelOfCar = editedModel;
            }
            else if(editChoose == "i" || editChoose == "I")
            {
                Console.Write("Write new state of this part: ");
                var editedIsUsed = Console.ReadLine();
                item.ModelOfCar = editedIsUsed;
            }
            else if (editChoose == "s" || editChoose == "S")
            {
                Console.Write("Write new sales of this part: ");
                var editedSales = Console.ReadLine();
                if (int.TryParse(editedSales, out var result))
                {
                    item.Sales = result;
                }
                else
                {
                    Console.WriteLine("This is not integer!");
                }
            }
            else if (editChoose == "p" || editChoose == "P")
            {
                Console.Write("Write new price of this part: ");
                var editedPrice = Console.ReadLine();
                if (int.TryParse(editedPrice, out var result))
                {
                    item.Sales = result;
                }
                else
                {
                    Console.WriteLine("This is not integer!");
                }
            }
        } 

        public void ShowPartById()
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

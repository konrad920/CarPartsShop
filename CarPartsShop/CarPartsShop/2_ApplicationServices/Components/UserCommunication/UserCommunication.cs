﻿using CarPartsShop.Data;
using CarPartsShop.Data.Entities;
using CarPartsShop.Data.Repositories;
using System.Text;

namespace CarPartsShop.Components.UserCommunication
{
    public class UserCommunication : IUserCommunication
    {
        private readonly IRepository<CarParts> _carPartsRepository;

        private readonly CarPartsDBContext _carPartsDBContext;

        public UserCommunication(IRepository<CarParts> carPartsRepository, CarPartsDBContext carPartsDBContext)
        {
            _carPartsRepository = carPartsRepository;
            _carPartsDBContext = carPartsDBContext;
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
            sb.AppendLine("(G) - Grouped data from DB");
            sb.Append("Select what do you want: ");

            return sb.ToString();
        }

        public string UserChoose()
        {
            var userChoose = Console.ReadLine();
            Console.WriteLine();
            return userChoose;
        }

        public CarParts CreateNewCarPart()
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
                throw new Exception("Wrong format of data");
            }
            Console.Write("Sales of this part: ");
            var sales = Console.ReadLine();
            if (int.TryParse(sales, out var result1))
            {
                carPart.Sales = result1;
            }
            else
            {
                throw new Exception("Wrong format of data");
            }
            carPart.NameOfPart = nameOfCarPart;
            carPart.ModelOfCar = nameOfCarModel;
            carPart.IsUsed = isUsed;
            return carPart;
        }

        public void RemovePart(CarParts partToRemove)
        {
            Console.WriteLine($"You want remove part: {partToRemove.NameOfPart}, at Id: {partToRemove.Id}");
            _carPartsRepository.Remove(partToRemove);
        }

        public void EditPart(CarParts partToEdit)
        {
            Console.WriteLine($"You want edit part: {partToEdit.NameOfPart}, at Id: {partToEdit.Id}");
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
                partToEdit.NameOfPart = editedName;
            }
            else if(editChoose == "m" || editChoose == "M")
            {
                Console.Write("Write new car model matched with this part: ");
                var editedModel = Console.ReadLine();
                partToEdit.ModelOfCar = editedModel;
            }
            else if(editChoose == "i" || editChoose == "I")
            {
                Console.Write("Write new state of this part: ");
                var editedIsUsed = Console.ReadLine();
                partToEdit.ModelOfCar = editedIsUsed;
            }
            else if (editChoose == "s" || editChoose == "S")
            {
                Console.Write("Write new sales of this part: ");
                var editedSales = Console.ReadLine();
                if (int.TryParse(editedSales, out var result))
                {
                    partToEdit.Sales = result;
                }
                else
                {
                    throw new Exception("This is not integer!");
                }
            }
            else if (editChoose == "p" || editChoose == "P")
            {
                Console.Write("Write new price of this part: ");
                var editedPrice = Console.ReadLine();
                if (int.TryParse(editedPrice, out var result))
                {
                    partToEdit.Sales = result;
                }
                else
                {
                    throw new Exception("This is not integer!");
                }
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
                throw new Exception("The base is empty");
            }
        }

        public CarParts GetPartById()
        {
            Console.Write("You want take part at Id: ");
            var IdToTake = Console.ReadLine();
            if (int.TryParse(IdToTake, out var result))
            {
                var itemToTake = _carPartsRepository.GetById(result);
                if (itemToTake != null)
                {
                    return itemToTake;
                }
                else
                {
                    throw new Exception("Id is not exist");
                    return null;
                }
            }
            else
            {
                throw new Exception("This Id is not integer!");
                return null;
            }
        }

        public void GroupedData()
        {
            var groups = _carPartsDBContext.CarParts
                        .GroupBy(x => x.ModelOfCar)
                        .Select(g => new
                        {
                            Name = g.Key,
                            CarParts = g.Select(c => new { c.Sales, c.NameOfPart, c.Price })
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
    }
}

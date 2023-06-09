using CarPartsShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.Components.UserCommunication
{
    public interface IUserCommunication
    {
        string BeginProgram();

        string UserChoose();

        CarParts AddNewCarPart();

        CarParts RemovePartId();

        CarParts GetPartByIDToEdit();

        void EditPart(CarParts item);

        void ShowPartById();

        void GetAllPart();
    }
}

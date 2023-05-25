using CarPartsShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPartsShop.UserCommunication
{
    public interface IUserCommunication
    {
        string BeginProgram();

        string UserChoose();

        void AddNewCarPart();

        void RemovePartId();

        void GetPartById();

        void GetAllPart();
    }
}

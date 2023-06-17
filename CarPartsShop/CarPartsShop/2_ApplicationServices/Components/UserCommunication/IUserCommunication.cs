using CarPartsShop.Data.Entities;


namespace CarPartsShop.Components.UserCommunication
{
    public interface IUserCommunication
    {
        string BeginProgram();

        string UserChoose();

        CarParts CreateNewCarPart();

        void RemovePart(CarParts partToRemove);

        void EditPart(CarParts partToRemove);

        CarParts GetPartById();

        void GetAllPart();

        void GroupedData();
    }
}

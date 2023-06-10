namespace CarPartsShop.Components.FileUser
{
    public interface IFileUser
    {
        void CreateFile(string fileName);

        void InsertDataFromFile(string fileName);
    }
}

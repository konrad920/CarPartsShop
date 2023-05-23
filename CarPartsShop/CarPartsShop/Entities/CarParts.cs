using System.Text;

namespace CarPartsShop.Entities
{
    public class CarParts : EntityBase
    {
        public string NameOfPart { get; set; }

        public string IsUsed { get; set; }

        public decimal Price { get; set; }

        public string ModelOfCar { get; set; }

        //calculating properties

        public string? NameLenght { get; set; }

        public int? TotalSales { get; set; }

        #region ToString Override
        public override string ToString()
        {
            StringBuilder sb = new(1024);
            sb.AppendLine($"Name of part: {NameOfPart}, part is {IsUsed}");
            sb.AppendLine($"Price for the part: {Price:c}");
            sb.AppendLine($"The part is matched to car: {ModelOfCar}");
            if ( NameLenght != null )
            {
                sb.AppendLine($"NameLenght: {NameLenght}");
            }
            if ( TotalSales != null )
            {
                sb.AppendLine($"Total sales: {TotalSales:c}");
            }

            return sb.ToString();
        }
        #endregion
    }
}

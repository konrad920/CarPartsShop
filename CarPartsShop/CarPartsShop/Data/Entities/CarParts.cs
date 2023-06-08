using System.Text;

namespace CarPartsShop.Data.Entities
{
    public class CarParts : EntityBase
    {
        public string NameOfPart { get; set; }

        public string IsUsed { get; set; }

        public decimal Price { get; set; }

        public string ModelOfCar { get; set; }

        //calculating properties

        public int? TotalSales { get; set; }

        //public override string ToString()
        //{
        //    StringBuilder sb = new(1024);
        //    sb.AppendLine($"Id of part: {this.Id}, Name of part: {NameOfPart}");
        //    sb.AppendLine($"Part is {IsUsed}, price for the part: {Price:c}");
        //    sb.AppendLine($"The part is matched to car: {ModelOfCar}");
        //    if (TotalSales != null)
        //    {
        //        sb.AppendLine($"Total sales: {TotalSales:c}");
        //    }

        //    return sb.ToString();
        //}
    }
}

namespace CarPartsShop.Entities
{
    public class CarParts : EntityBase
    {
        public string? NameOfPart { get; set; }
        public override string ToString() => $"Id part: {base.Id}, Name of part: {NameOfPart}";
    }
}

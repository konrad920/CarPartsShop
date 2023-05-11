namespace CarPartsShop.Entities
{
    public class CarParts
    {
        public int Id = 0;
        public string? NameOfPart { get; set; }

        public CarParts() { }
        public override string ToString() => $"Id part: {Id}, Name of part: {NameOfPart}";
    }
}

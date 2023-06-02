namespace CarPartsShop.Data.Entities
{
    public class MotoParts : CarParts
    {
        public override string ToString() => $"Id part: {Id}, Name of part: {NameOfPart}" + " The motorbike";
    }
}

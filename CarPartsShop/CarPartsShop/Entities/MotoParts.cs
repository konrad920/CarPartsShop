namespace CarPartsShop.Entities
{
    public class MotoParts : CarParts
    {
        public override string ToString() => $"Id part: {base.Id}, Name of part: {NameOfPart}" + " The motorbike";
    }
}

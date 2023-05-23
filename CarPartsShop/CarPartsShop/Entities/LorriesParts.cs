namespace CarPartsShop.Entities
{
    public class LorriesParts : CarParts
    {
        public override string ToString() => $"Id part: {base.Id}, Name of part: {NameOfPart}" + " The lorry";
    }
}

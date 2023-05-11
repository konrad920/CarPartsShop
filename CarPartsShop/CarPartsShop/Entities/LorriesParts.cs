namespace CarPartsShop.Entities
{
    public class LorriesParts
    {
        public int Id { get; set; }
        public string NameOfPart { get; set; }

        public override string ToString() => $"Name of part: {NameOfPart}";
    }
}

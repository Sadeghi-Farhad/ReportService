namespace ReportService.Domain.Users
{
    public class AddressValueObject : ValueObject
    {
        public AddressValueObject()
        {
            Province = City = Street = PostalCode = string.Empty;
        }

        public AddressValueObject(string province, string city, string street, string? postalcode = null)
        {
            Province = province;
            City = city;
            Street = street;
            PostalCode = postalcode;
        }

        public string Province { get; init; }
        public string City { get; init; }
        public string Street { get; init; }
        public string? PostalCode { get; init; }

        public override string ToString()
        {
            return $"{Province},{City},{Street},PostalCode:{PostalCode ?? "-"}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ToString();
        }
    }
}

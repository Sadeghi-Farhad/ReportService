using ReportService.Domain.Exceptions;

namespace ReportService.Domain.Users
{
    /// <summary>
    /// User Aggregate Root class.
    /// </summary>
    public partial class User : IAggregateRoot
    {
        public User(string name, DateOnly birthday, string email)
            : this()
        {
            Update(name, birthday, email);
        }

        public void Update(string name, DateOnly birthday, string email)
        {
            if (birthday > DateOnly.FromDateTime(DateTime.Now))
                throw new BaseException("Birthday could not be after today!");

            Name = name;
            Birthday = birthday;
            Email = email;
        }

        public void SetAddress(string province, string city, string street, string postalcode)
        {
            Address = new AddressValueObject(province, city, street, postalcode);
        }
    }
}
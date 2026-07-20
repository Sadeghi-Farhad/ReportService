
namespace ReportService.Domain.Personnels
{
    public class Personnel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int PersonnelCode { get; set; }
        public bool Active { get; set; }
    }
}

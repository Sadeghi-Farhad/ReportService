namespace ReportService.Domain.Personnels
{
    public interface IPersonnelInfo
    {
        Task<List<Personnel>> GetPersonnels(PersonnelSearchInput input);
    }
}

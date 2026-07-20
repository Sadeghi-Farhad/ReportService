using System.ComponentModel;

namespace ReportService.Domain.Personnels
{
    public class PersonnelSearchInput
    {
        public List<int> Srls { get; set; } = [];
        public List<int> PrsCodes { get; set; } = [];
        public List<int> UnitIds { get; set; } = [];
        public List<int> ManagerPrsCodes { get; set; } = [];
        public List<byte> PlantIds { get; set; } = [];
        public string Name { get; set; } = string.Empty;
        [DefaultValue(1)]
        public byte Active { get; set; }

        public PersonnelSearchInput(List<int> prsCodes)
        {
            PrsCodes = prsCodes;
            Active = 2;
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace ARMApocalypseSASAPI.Models
{
    public class SurvivorInfectionReport : BaseClass
    {
        public string ReportingSurvivorID { get; set; } //who is reporting this current Survivor as Infected...
        [ForeignKey(nameof(ReportingSurvivorID))]
        public Survivor Reporter { get; set; } 
        public string ReportedSurvivorID { get; set; } // who was reported as being INFECTED CURRENTLY...
        [ForeignKey(nameof(ReportedSurvivorID))]
        public Survivor ReportedSurvivor { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime DateOfReport { get; set; }
    }
}

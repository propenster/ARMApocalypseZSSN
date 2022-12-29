namespace ARMApocalypseSASAPI.Dtos
{
    public class UpdateSurvivorInfectionStatusRequest
    {
        public bool IsInfected { get; set; }
        public string ReportingSurvivorID { get; set; } = string.Empty; //who is reporting this current Survivor as Infected...
        public string ReportedSurvivorID { get; set; } = string.Empty; // who was reported as being INFECTED CURRENTLY...
        public string Notes { get; set; } = string.Empty;
        public DateTime DateOfReport { get; set; }
    }
}

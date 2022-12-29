namespace ARMApocalypseSASAPI.Dtos
{
    public class SurvivorInfectionReportResponse
    {
        public string ReportId { get; set; } = string.Empty;
        public SurvivorResponse Reporter { get; set; } = new();
        public SurvivorResponse ReportedSurvivor { get; set; } = new();
        public string Notes { get; set; } = string.Empty;
        public DateTime DateOfReport { get; set; }
    }
}

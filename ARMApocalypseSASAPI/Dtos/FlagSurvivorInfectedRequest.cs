namespace ARMApocalypseSASAPI.Dtos
{
    public class FlagSurvivorInfectedRequest
    {
        public string SurvivorId { get; set; } = string.Empty;
        public bool? IsInfected { get; set; } = null;
    }
}

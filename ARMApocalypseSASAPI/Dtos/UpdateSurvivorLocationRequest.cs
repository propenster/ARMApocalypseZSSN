namespace ARMApocalypseSASAPI.Dtos
{
    public class UpdateSurvivorLocationRequest
    {
        public string SurvivorId { get; set; } = string.Empty;
        public double LastLocationLongitude { get; set; }
        public double LastLocationLatitude { get; set; }
    }
}

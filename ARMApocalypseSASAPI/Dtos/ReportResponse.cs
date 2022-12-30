namespace ARMApocalypseSASAPI.Dtos
{
    public class ReportResponse
    {
        public decimal InfectedSurvivorsPercent { get; set; }
        public decimal NonInfectedSurvivorsPercent { get; set; }
        public List<ItemAverageResponse> ItemAverageResponses { get; set; } 
        public decimal PointsLostDueToInfection { get; set; }
    }
    public class ItemAverageResponse
    {
        public string ItemName { get; set; } = string.Empty;
        public decimal Average { get; set; }
        public decimal Total { get;  set; }
    }
}

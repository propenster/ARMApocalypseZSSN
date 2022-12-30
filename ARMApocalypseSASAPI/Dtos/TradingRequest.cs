namespace ARMApocalypseSASAPI.Dtos
{
    public class TradingRequest
    {
        public string BuyerSurvivorId { get; set; } = string.Empty;
        public string SellerSurvivorId { get; set; } = string.Empty;
        public ICollection<CreateTradeItemRequest> BuyerItems { get; set; } = new List<CreateTradeItemRequest>();
        public ICollection<CreateTradeItemRequest> SellerItems { get; set; } = new List<CreateTradeItemRequest>();


    }
}

namespace ARMApocalypseSASAPI.Dtos
{
    public class TradeItemResponse : BaseResponse
    {
        public string TradeItemId { get; set; }
        public ItemResponse Item { get; set; } = new();
        public SurvivorResponse Survivor { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPoints { get; set; }
        //public string ItemName { get; set; } = string.Empty;
    }
}

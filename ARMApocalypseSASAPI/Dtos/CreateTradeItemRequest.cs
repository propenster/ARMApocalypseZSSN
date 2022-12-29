namespace ARMApocalypseSASAPI.Dtos
{
    public class CreateTradeItemRequest
    {
        public string ItemId { get; set; } = string.Empty; //ForeignKey
        public string SurvivorId { get; set; } //the owner (Survivor) of this TradeItem
        public int Quantity { get; set; }
        public decimal UnitPoints { get; set; }
        public string ItemName { get; set; } = string.Empty;
    }
}

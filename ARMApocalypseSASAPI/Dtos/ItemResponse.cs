namespace ARMApocalypseSASAPI.Dtos
{
    public class ItemResponse : BaseResponse
    {
        public string ItemId { get; set; } = string.Empty; //item's PrimaryKey typeof(GUID).
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ARMApocalypseSASAPI.Dtos
{
    public class SurvivorResponse : BaseResponse
    {
        public bool IsInfected { get; set; }
        public string SurvivorId { get; set; } //Survivor PK_GUID
        [Required, MaxLength(100), MinLength(3)]
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        [Required, MinLength(1), MaxLength(1)]
        public string Gender { get; set; } = string.Empty;
        public double LastLocationLongitude { get; set; } = 0;
        public double LastLocationLatitude { get; set; } = 0;
        public ICollection<TradeItemResponse> OwnTradeItems { get; set; } = new List<TradeItemResponse>();
    }
}

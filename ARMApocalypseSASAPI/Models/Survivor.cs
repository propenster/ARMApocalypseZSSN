using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARMApocalypseSASAPI.Models
{
    public class Survivor : BaseClass
    {       
        [Required, MaxLength(100), MinLength(3)]
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        [Required, MinLength(1), MaxLength(1)]
        public string Gender { get; set; } = string.Empty;
        public double LastLocationLongitude { get; set; } = 0;
        public double LastLocationLatitude { get; set; } = 0;
        public ICollection<TradeItem> OwnTradeItems { get; set; } = new List<TradeItem>();  
        public bool IsInfected { get; set; }

        //public SurvivorProperty SurvivorProperty { get; set; }
    }
}

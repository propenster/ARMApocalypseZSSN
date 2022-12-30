using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARMApocalypseSASAPI.Models
{
    //PROPERTY of a SURVIVOR
    public class TradeItem : BaseClass
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public string Id { get; set; }
        [ForeignKey(nameof(ItemId))]
        public Item Item { get; set; } = new();
        public string ItemId { get; set; } = string.Empty; //ForeignKey
        public string SurvivorId { get; set; } //the owner (Survivor) of this TradeItem
        [ForeignKey(nameof(SurvivorId))]
        public Survivor Survivor { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPoints { get; set; }
        //public string ItemName { get; set; } = string.Empty;
    }
}

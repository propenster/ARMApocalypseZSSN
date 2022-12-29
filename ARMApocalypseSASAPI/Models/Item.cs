using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARMApocalypseSASAPI.Models
{
    public class Item : BaseClass
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public string Id { get; set; }
        public string Name { get; set; } = string.Empty;    
        public decimal Price { get; set; }
    }
}

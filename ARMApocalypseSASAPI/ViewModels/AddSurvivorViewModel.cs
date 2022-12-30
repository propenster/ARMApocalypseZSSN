using System.ComponentModel.DataAnnotations;

namespace ARMApocalypseSASAPI.ViewModels
{
    public class AddSurvivorViewModel
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        [Required, MinLength(1), MaxLength(1)]
        public string Gender { get; set; } = string.Empty;
        public double LastLocationLongitude { get; set; } = 0;
        public double LastLocationLatitude { get; set; } = 0;
    }
}

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ARMApocalypseSASAPI.Helpers
{
    public class DefaultErrorResponse
    {
        //[RegularExpression(@"(?:^.*[^a-zA-Z0-9]|^)")]
        [MaxLength(255), RegularExpression(@"^.*")]
        public string Error { get; set; } = string.Empty;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}

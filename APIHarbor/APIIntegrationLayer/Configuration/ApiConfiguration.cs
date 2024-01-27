using System.ComponentModel.DataAnnotations;

namespace APIHarbor.APIIntegrationLayer.Configuration
{
    public class ApiConfiguration
    {
        [Required]
        public string? Endpoint { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public string? ApiKey { get; set; }
        public string? AuthToken { get; set; }
        public int? Timeout { get; set; }
    }
}

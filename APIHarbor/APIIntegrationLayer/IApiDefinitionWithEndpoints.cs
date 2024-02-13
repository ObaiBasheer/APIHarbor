using APIHarbor.APIIntegrationLayer.Configuration;
using System.Net;

namespace APIHarbor.APIIntegrationLayer
{
    public interface IApiDefinitionWithEndpoints
    {
        List<ApiConfiguration> Endpoints { get; set; }
    }
}

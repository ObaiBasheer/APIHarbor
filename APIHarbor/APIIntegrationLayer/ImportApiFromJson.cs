using System.Text.Json;

namespace APIHarbor.APIIntegrationLayer
{

    public class ImportApiFromJson
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public ImportApiFromJson(HttpClient httpClient, string apiUrl)
        {
            _httpClient = httpClient;
            _apiUrl = apiUrl;
        }

        public async Task<T> ImportAsync<T>()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiUrl);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var result = JsonSerializer.Deserialize<T>(json, options);

                return result;
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors
                Console.WriteLine($"HTTP request error: {ex.Message}");
                throw;
            }
            catch (JsonException ex)
            {
                // Handle JSON deserialization errors
                Console.WriteLine($"JSON deserialization error: {ex.Message}");
                throw;
            }
        }


        public async Task FetchDataAsync<T>(T apiDefinition)
        {
            // Assume T has a property named 'Endpoints' that represents a collection of API endpoints
            if (apiDefinition is IApiDefinitionWithEndpoints definitionWithEndpoints)
            {
                foreach (var endpoint in definitionWithEndpoints.Endpoints)
                {
                    // Assume each endpoint has a URL property
                    var endpointUrl = endpoint.Url;

                    // Make a request to the endpoint
                    var endpointResponse = await _httpClient.GetAsync(endpointUrl);
                    endpointResponse.EnsureSuccessStatusCode();

                    // Do something with the endpoint response, e.g., log or process the data
                    var responseData = await endpointResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response from {endpointUrl}: {responseData}");
                }
            }
            else
            {
                // Handle the case where the provided API definition doesn't have endpoints
                Console.WriteLine("API definition does not contain endpoints.");
            }

            // You can customize this method based on the actual structure of your API definition.
            // Add logic to handle different types of API definitions and perform relevant operations.
        }

    }


}

using System.Text;
using CircuitBreaker.ApiModels;
using Newtonsoft.Json;
using Polly.CircuitBreaker;

namespace CircuitBreaker.Services
{
    public interface IHttpClientService
    {
        Task<List<GetQuantityResponse>> GetAsync();
    }

    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClientFactory;

        public HttpClientService(HttpClient httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<GetQuantityResponse>> GetAsync()
        {
            GetQuantityRequest content = new();
            content.Items.Add("33000135001");
            content.Items.Add("33000135002");

            var request = new HttpRequestMessage(HttpMethod.Get, "http://10.30.35.15:5247/Product/GetQuantity")
            {
                Content = CreateHttpContent(content)
            };

            _httpClientFactory.DefaultRequestHeaders.Add("UserName", "pindistest");
            _httpClientFactory.DefaultRequestHeaders.Add("Password", "123456");

            try
            {
                _httpClientFactory.Timeout = TimeSpan.FromSeconds(20);
                var response = await _httpClientFactory.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<GetQuantityResponse>>(data);
            }
            catch (BrokenCircuitException ex)
            {
                throw ex;
            }


        }

        private static HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }
    }
}
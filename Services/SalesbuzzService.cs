using CircuitBreaker.ApiModels;

namespace CircuitBreaker.Services
{
    public interface ISalesbuzzService
    {
        Task<List<GetQuantityResponse>> GetQuantities();
    }

    public class SalesbuzzService : ISalesbuzzService
    {
        private readonly IHttpClientService _httpClientService;

        public SalesbuzzService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<List<GetQuantityResponse>> GetQuantities()
        {
            return await _httpClientService.GetAsync();
        }
    }
}
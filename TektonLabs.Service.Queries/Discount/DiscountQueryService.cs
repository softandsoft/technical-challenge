


using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text;
using TektonLabs.Domain;
using TektonLabs.Service.Queries.Product;

namespace TektonLabs.Service.Queries.Discount
{
    public interface IDiscountQueryService
    {
        Task<List<Domain.Entities.Discount>> GetDiscounts();
    }

    public class DiscountQueryService : IDiscountQueryService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DiscountQueryService> _logger;

        public DiscountQueryService(IConfiguration configuration, ILogger<DiscountQueryService> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<List<Domain.Entities.Discount>> GetDiscounts()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var clientResponse = await httpClient.GetAsync(_configuration.GetSection("Services:APIDiscounts").Value);

                    var responseContent = await clientResponse.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var response = JsonSerializer.Deserialize<List<Domain.Entities.Discount>>(responseContent, options);

                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

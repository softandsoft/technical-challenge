
using TektonLabs.Domain;
using TektonLabs.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AutoMapper;
using TektonLabs.Domain.DTOs.Response;
using TektonLabs.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace TektonLabs.Service.Queries.Company
{
    public interface IProductQueryService
    {
        Task<WebApiResponse> GetByIdAsync(string id);
        Task<List<Status>> GetStatusAsync();

    }

    public class ProductQueryService : IProductQueryService
    {
        private readonly ILogger<ProductQueryService> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public ProductQueryService(
            ILogger<ProductQueryService> logger,
            IProductRepository productRepository,
            IMapper mapper,
            IMemoryCache memoryCache
        )
        {
            _logger = logger;
            _productRepository = productRepository;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<WebApiResponse> GetByIdAsync(string id)
        {
            MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
            cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
            cacheExpirationOptions.Priority = CacheItemPriority.Normal;

            WebApiResponse response = new WebApiResponse();

            try
            {
                var entity = await _productRepository.GetByIdAsync(id);

                var product = _mapper.Map<ProductResponse>(entity);

                if (product == null)
                {
                    response.Success = false;
                    response.Errors = new List<Error>
                    {
                        new Error()
                        {
                            Code = StatusCodes.Status404NotFound,
                            Message = $"No existe el producto con el id {id}."
                        }
                    };

                    return response;
                }

                List<Status> status = await _memoryCache.GetOrCreate("status", cacheEntry => {
                    return GetStatusAsync();
                });

                product.StatusName = status.FirstOrDefault(x => x.Id == entity.Status).Name;
                product.Discount = 30;
                product.FinalPrice = product.Price * (100 - product.Discount) / 100;

                response.Success = true;
                response.Response = new Response();
                response.Response.Data = product;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                response.Success = false;
                response.Errors = new List<Error>
                {
                    new Error()
                    {
                        Code = StatusCodes.Status500InternalServerError,
                        Message = ex.Message
                    }
                };

                return response;
            }
        }

        public async Task<List<Status>> GetStatusAsync()
        {
            List<Status> status = new List<Status>
            {
                new Status() { Id = "1", Name = "Active" },
                new Status() { Id = "0", Name = "Inactive" }
            };

            return status;
        }
    }
}



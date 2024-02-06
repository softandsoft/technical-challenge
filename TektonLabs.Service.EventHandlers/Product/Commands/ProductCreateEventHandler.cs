using AutoMapper;
using TektonLabs.Domain;
using TektonLabs.Domain.DTOs.Request;
using TektonLabs.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TektonLabs.Service.EventHandlers.Company.Validations;
using TektonLabs.Service.EventHandlers.Company.Commands;
using TektonLabs.Service.EventHandlers.Product.Validations;

namespace TektonLabs.Service.EventHandlers.Product.Commands
{
    public class ProductCreateEventHandler : IRequestHandler<ProductCreateCommand, WebApiResponse>
    {
        private readonly ILogger<ProductCreateEventHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductCreateEventHandler(
            ILogger<ProductCreateEventHandler> logger,
            IMapper mapper,
            IProductRepository productRepository
        )
        {
            _logger = logger;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<WebApiResponse> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            WebApiResponse response = new WebApiResponse();

            try
            {
                var productCreateRequest = _mapper.Map<ProductCreateRequest>(request);

                var validator = new ProductCreateValidation();
                var result = validator.Validate(productCreateRequest);

                if (!result.IsValid)
                {
                    response.Success = false;
                    response.Errors = new List<Error>();

                    Error error = null;

                    foreach (var item in result.Errors)
                    {
                        error = new Error();

                        error.Code = StatusCodes.Status400BadRequest;
                        error.Message = item.ErrorMessage;

                        response.Errors.Add(error);
                    }

                    return response;
                }

                await _productRepository.InsertAsync(productCreateRequest);

                if (productCreateRequest == null)
                {
                    response.Success = false;
                    response.Errors = new List<Error>
                    {
                        new Error()
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Message = $"La compañía {productCreateRequest.Name} no pudo ser registrada."
                        }
                    };

                    return response;
                }

                response.Success = true;
                response.Response = new Response();
                response.Response.Data = productCreateRequest;

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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TektonLabs.Domain.DTOs.Request;
using TektonLabs.Domain.Interfaces;
using TektonLabs.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AutoMapper;
using TektonLabs.Service.EventHandlers.Company.Validations;


namespace TektonLabs.Service.EventHandlers.Company.Commands
{
    public class ProductUpdateEventHandler : IRequestHandler<ProductUpdateCommand, WebApiResponse>
    {
        private readonly ILogger<ProductUpdateEventHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductUpdateEventHandler(
            ILogger<ProductUpdateEventHandler> logger,
            IMapper mapper,
            IProductRepository productRepository
        )
        {
            _logger = logger;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<WebApiResponse> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            WebApiResponse response = new WebApiResponse();

            try
            {
                var productUpdateRequest = _mapper.Map<ProductUpdateRequest>(request);

                var validator = new ProductUpdateValidation();
                var result = validator.Validate(productUpdateRequest);

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

                await _productRepository.UpdateAsync(productUpdateRequest);

                if (productUpdateRequest == null)
                {
                    response.Success = false;
                    response.Errors = new List<Error>
                    {
                        new Error()
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Message = $"El rol {productUpdateRequest.ProductId} no pudo ser actualizado."
                        }
                    };

                    return response;
                }

                response.Success = true;
                response.Response = new Response();
                response.Response.Data = productUpdateRequest;

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

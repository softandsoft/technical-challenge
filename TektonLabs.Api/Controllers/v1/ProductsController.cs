
using TektonLabs.Domain;
using TektonLabs.Service.EventHandlers.Company.Commands;
using TektonLabs.Service.Queries.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TektonLabs.Domain.DTOs.Response;

namespace TektonLabs.Api.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductQueryService _productQueryService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMediator _mediator;

        public ProductsController(IProductQueryService productQueryService, 
            IConfiguration configuration, 
            ILogger<ProductsController> logger, 
            IMediator mediator, 
            IMemoryCache memoryCache)
        {
            _productQueryService = productQueryService;
            _configuration = configuration;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WebApiResponse>> GetById(string id)
        {
            DateTime startTime = DateTime.Now;

            WebApiResponse response = new WebApiResponse();
            response = await _productQueryService.GetByIdAsync(id);

            DateTime endTime = DateTime.Now;
            TimeSpan ts = endTime.Subtract(startTime);
            _logger.LogInformation(string.Format("Tiempo de respuesta de GetByIdAsync: {0} segundos", ts.TotalSeconds));

            if (response.Success)
                return StatusCode(StatusCodes.Status200OK, response);
            else
                return StatusCode(response.Errors[0].Code, response);
        }

        [HttpPost]
        public async Task<ActionResult<WebApiResponse>> Create([FromBody] ProductCreateCommand command)
        {
            DateTime startTime = DateTime.Now;

            WebApiResponse response = new WebApiResponse();

            response = await _mediator.Send(command);

            DateTime endTime = DateTime.Now;
            TimeSpan ts = endTime.Subtract(startTime);
            _logger.LogInformation(string.Format("Tiempo de respuesta de Create: {0} segundos", ts.TotalSeconds));

            if (response.Success)
                return StatusCode(StatusCodes.Status201Created, response);
            else
                return StatusCode(response.Errors[0].Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WebApiResponse>> Update(string id, [FromBody] ProductUpdateCommand command)
        {
            DateTime startTime = DateTime.Now;

            WebApiResponse response = new WebApiResponse();

            command.ProductId = id;

            response = await _mediator.Send(command);

            DateTime endTime = DateTime.Now;
            TimeSpan ts = endTime.Subtract(startTime);
            _logger.LogInformation(string.Format("Tiempo de respuesta de Update: {0} segundos", ts.TotalSeconds));

            if (response.Success)
                return StatusCode(StatusCodes.Status200OK, response);
            else
                return StatusCode(response.Errors[0].Code, response);
        }   
    }
}

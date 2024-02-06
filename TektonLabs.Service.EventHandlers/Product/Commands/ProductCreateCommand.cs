using TektonLabs.Domain;
using MediatR;

namespace TektonLabs.Service.EventHandlers.Company.Commands
{
    public class ProductCreateCommand : IRequest<WebApiResponse>
    {        
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string CreationUser { get; set; }
    }
}

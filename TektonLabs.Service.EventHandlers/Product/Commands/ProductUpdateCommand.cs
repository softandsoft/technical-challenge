using TektonLabs.Domain;
using MediatR;

namespace TektonLabs.Service.EventHandlers.Company.Commands
{
    public class ProductUpdateCommand : IRequest<WebApiResponse>
    {
        public string? ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string ModificationUser { get; set; }
    }
}

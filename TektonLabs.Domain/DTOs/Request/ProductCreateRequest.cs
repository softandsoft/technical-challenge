namespace TektonLabs.Domain.DTOs.Request
{
    public class ProductCreateRequest
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string CreationUser { get; set; }
    }
}
namespace TektonLabs.Domain.DTOs.Response
{
    public class ProductResponse
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        //public string Status { get; set; }
        public string StatusName { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationUser { get; set; }
    }
}

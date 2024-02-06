using TektonLabs.Domain.DTOs.Request;
using TektonLabs.Domain.Entities;

namespace TektonLabs.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(string id);
        Task InsertAsync(ProductCreateRequest request);
        Task UpdateAsync(ProductUpdateRequest request);
    }
}

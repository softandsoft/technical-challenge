using Dapper;
using TektonLabs.Domain.DTOs.Request;
using TektonLabs.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using TektonLabs.Domain.Entities;

namespace TektonLabs.Persistence.Database.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            try
            {
                Product entity = new Product();

                string storedProcedure = "[dbo].[SelectProductById]";

                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DbTektonLabsConnection")))
                {
                    entity = await connection.QueryFirstOrDefaultAsync<Product>(storedProcedure, new { ProductId = id }, commandType: CommandType.StoredProcedure);
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task InsertAsync(ProductCreateRequest request)
        {
            try
            {
                string storedProcedure = "[dbo].[InsertProduct]";

                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DbTektonLabsConnection")))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ProductId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    parameters.Add("@Name", request.Name);
                    parameters.Add("@Description", request.Description);
                    parameters.Add("@Stock", request.Stock);
                    parameters.Add("@Price", request.Price);
                    parameters.Add("@Status", request.Status);
                    parameters.Add("@CreationUser", request.CreationUser);

                    int result = await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                    request.ProductId = parameters.Get<int>("@ProductId");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAsync(ProductUpdateRequest request)
        {
            try
            {
                string storedProcedure = "[dbo].[UpdateProduct]";

                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DbTektonLabsConnection")))
                {
                    object parameters = new
                    {
                        ProductId = request.ProductId,
                        Name = request.Name,
                        Description = request.Description,
                        Stock = request.Stock,
                        Price = request.Price,
                        Status = request.Status,
                        ModificationUser = request.ModificationUser
                    };

                    int result = await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


using Micro.Web.Models;

namespace Micro.Web.Services.Abstract
{
    public interface IProductService
    {
        Task<T> GetAllProductsAsync<T>(string token);
        Task<T> GetProductByIdAsync<T>(int id, string token);
        Task<T> CreateProductAsync<T>(ProductDTO productDto, string token);
        Task<T> UpdateProductAsync<T>(ProductDTO productDto, string token);
        Task<T> DeleteProductAsync<T>(int id, string token);
    }
}

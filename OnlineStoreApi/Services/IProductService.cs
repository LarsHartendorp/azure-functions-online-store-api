using OnlineStoreApi.Models.DTO.Requests.Product;
using OnlineStoreApi.Models.DTO.Responses.Product;
using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(string id);
        Task<CreateProductResponse> AddProductAsync(CreateProductRequest request);
        Task<PutProductByIdResponse> UpdateProductByIdAsync(string id, PutProductByIdRequest request);
        Task<GetProductByIdResponse> GetProductByIdResponseAsync(string id);
        Task<IEnumerable<GetProductByIdResponse>> GetAllProductsResponseAsync(); 
        Task DeleteProductAsync(string id);
        Task<bool> ProductExistsAsync(string id);
    }
}

using OnlineStoreApi.Models.DTO.Requests.Product;
using OnlineStoreApi.Models.DTO.Responses.Product;
using OnlineStoreAPI.Models;
using OnlineStoreAPI.Repositories;

namespace OnlineStoreAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }
        public async Task<IEnumerable<GetProductByIdResponse>> GetAllProductsResponseAsync()
        {
            var products = await GetAllProductsAsync();
            return products.Select(product => new GetProductByIdResponse
            {
                ProductId = product.ProductId.ToString(),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            }).ToList(); 
        }

        public async Task<Product?> GetProductByIdAsync(string id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<GetProductByIdResponse> GetProductByIdResponseAsync(string id)
        {
            var product = await GetProductByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found");
            }

            var response = new GetProductByIdResponse
            {
                ProductId = product.ProductId.ToString(),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };

            return response;
        }

        public async Task<CreateProductResponse> AddProductAsync(CreateProductRequest request)
        {
            Product product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ImageUrl = request.ImageUrl
            };

            await _productRepository.AddAsync(product);

            return new CreateProductResponse
            {
                ProductId = product.ProductId.ToString(),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };
        }
        public async Task<PutProductByIdResponse> UpdateProductByIdAsync(string id, PutProductByIdRequest request)
        {
            var exists = await ProductExistsAsync(id);
            if (!exists)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found");
            }

            var existingProduct = await GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found");
            }

            existingProduct.Name = request.Name;
            existingProduct.Description = request.Description;
            existingProduct.Price = request.Price;
            existingProduct.ImageUrl = request.ImageUrl;

            await _productRepository.UpdateAsync(existingProduct);

            var response = new PutProductByIdResponse
            {
                ProductId = existingProduct.ProductId.ToString(),
                Name = existingProduct.Name,
                Description = existingProduct.Description,
                Price = existingProduct.Price,
                ImageUrl = existingProduct.ImageUrl
            };

            return response;
        }
        
        public async Task DeleteProductAsync(string id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<bool> ProductExistsAsync(string id)
        {
            return await _productRepository.ExistsAsync(id);
        }
    }
}

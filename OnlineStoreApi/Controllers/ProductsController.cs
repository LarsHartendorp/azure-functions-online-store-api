using Microsoft.AspNetCore.Mvc;
using OnlineStoreApi.Models.DTO.Requests.Product;
using OnlineStoreApi.Models.DTO.Responses.Product;
using OnlineStoreAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductByIdResponse>>> GetProducts()
        {
            var responses = await _productService.GetAllProductsResponseAsync();
            return Ok(responses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductByIdResponse>> GetProduct(string id)
        {
            var response = await _productService.GetProductByIdResponseAsync(id);
            if (response == null)
            {
                return NotFound(new { message = $"Product with ID {id} not found" });
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CreateProductResponse>> PostProduct(CreateProductRequest request)
        {
            var response = await _productService.AddProductAsync(request);
            return CreatedAtAction(nameof(GetProduct), new { id = response.ProductId }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PutProductByIdResponse>> PutProduct(string id, PutProductByIdRequest request)
        {
            var exists = await _productService.ProductExistsAsync(id);
            if (!exists)
            {
                return NotFound(new { message = $"Product with ID {id} not found" });
            }

            var response = await _productService.UpdateProductByIdAsync(id, request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var exists = await _productService.ProductExistsAsync(id);
            if (!exists)
            {
                return NotFound(new { message = $"Product with ID {id} not found" });
            }

            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}

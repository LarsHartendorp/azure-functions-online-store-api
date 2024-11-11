using Microsoft.AspNetCore.Mvc;
using OnlineStoreApi.Models.DTO.Requests.Product;
using OnlineStoreApi.Models.DTO.Responses.Product;
using OnlineStoreAPI.Services;

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
            try
            {
                var responses = await _productService.GetAllProductsResponseAsync();
                return Ok(responses);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetProductByIdResponse>> GetProduct(string id)
        {
            try
            {
                var response = await _productService.GetProductByIdResponseAsync(id);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<CreateProductResponse> PostProduct(CreateProductRequest request)
        {
             return await _productService.AddProductAsync(request);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PutProductByIdResponse>> PutProduct(string id, PutProductByIdRequest request)
        {
            try
            {
                var response = await _productService.UpdateProductByIdAsync(id, request);
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the product"});
            }
        }
    }
}

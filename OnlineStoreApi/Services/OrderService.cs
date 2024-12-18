﻿using MongoDB.Bson;
using OnlineStoreApi.Models.DTO.Requests.Order;
using OnlineStoreApi.Models.DTO.Responses.Order;
using OnlineStoreAPI.Models;
using OnlineStoreAPI.Repositories;

namespace OnlineStoreAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;

        public OrderService(IOrderRepository orderRepository, IProductService productService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
        }

        public async Task<IEnumerable<GetOrderByIdResponse>> GetOrdersWithResponsesAsync()
        {
            var orders = await _orderRepository.GetOrdersAsync();
            var responses = orders.Select(order => new GetOrderByIdResponse
            {
                OrderId = order.OrderId.ToString(),
                OrderDate = order.OrderDate,
                ShippingDate = order.ShippingDate,
                UserId = order.UserId.ToString(),
                ProductIds = order.Products.Select(p => p.ProductId.ToString()).ToList()
            });
            return responses;
        }

        public async Task<GetOrderByIdResponse?> GetOrderResponseByIdAsync(string id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null) return null;

            return new GetOrderByIdResponse
            {
                OrderId = order.OrderId.ToString(),
                OrderDate = order.OrderDate,
                ShippingDate = order.ShippingDate,
                UserId = order.UserId.ToString(),
                ProductIds = order.Products.Select(p => p.ProductId.ToString()).ToList()
            };
        }

        public async Task<CreateOrderResponse> AddOrderAsync(CreateOrderRequest request)
        {
            List<Product> products = new List<Product>();
            request.ProductIds.ForEach(async id =>
            {
                Product? product = await _productService.GetProductByIdAsync(id);
                if (product != null)
                {
                    products.Add(product);
                }
            });

            Order order = new Order
            {
                OrderId = ObjectId.GenerateNewId(),
                OrderDate = DateTime.Now,
                ShippingDate = null,
                UserId = ObjectId.Parse(request.UserId),
                Products = products
            };

            await _orderRepository.AddOrderAsync(order);

            return new CreateOrderResponse { 
                OrderId = order.OrderId.ToString(),
                OrderDate = order.OrderDate,
                UserId = order.UserId.ToString(),
                ProductIds = order.Products.Select(p => p.ProductId.ToString()).ToList()
            };
        }

        public async Task UpdateOrderAsync(string orderId, DateTime shippingDate)
        {
            Order? order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) 
            {
                throw new Exception($"Order with ID {orderId} not found");
            }
            
            order.ShippingDate = shippingDate;
            
            await _orderRepository.UpdateOrderAsync(order);
        }

        public Task DeleteOrderAsync(string id)
        {
            return _orderRepository.DeleteOrderAsync(id);
        }

        public Task<bool> OrderExistsAsync(string id)
        {
            return _orderRepository.OrderExistsAsync(id);
        }
    }
}

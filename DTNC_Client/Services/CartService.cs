using DuAn_DTNC.Models;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DuAn_DTNC.Services
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync();
        Task<string> GetUserIdFromTokenAsync();
    }

    public class TokenService : ITokenService
    {
        private readonly IJSRuntime _jsRuntime;

        public TokenService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");
        }

        public async Task<string> GetUserIdFromTokenAsync()
        {
            var token = await GetTokenAsync();

            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("Token not found");

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "sub" || claim.Type == "userId");

            if (userIdClaim == null)
                throw new InvalidOperationException("UserId not found in token");

            if (!int.TryParse(userIdClaim.Value, out int userId))
                throw new InvalidOperationException("UserId in token is not in a valid format");

            return userIdClaim.Value;
        }
    }

    public interface ICartService
    {
        IEnumerable<OrderItem> GetCartItems();
        Task AddToCart(FoodItem item, int quantity = 1);
    }

    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _tokenService;

        public CartService(HttpClient httpClient, ITokenService tokenService)
        {
            _httpClient = httpClient;
            _tokenService = tokenService;
        }

        private async Task SetAuthorizationHeaderAsync()
        {
            var token = await _tokenService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public IEnumerable<OrderItem> GetCartItems()
        {
            throw new NotImplementedException();
        }

        public async Task AddToCart(FoodItem item, int quantity = 1)
        {
            try
            {
                await SetAuthorizationHeaderAsync();

                var token = await _tokenService.GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Token is null or empty");
                }

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var userIdString = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

                if (!int.TryParse(userIdString, out int userId))
                {
                    throw new Exception("UserId in token is not in a valid format");
                }

                Console.WriteLine($"User ID: {userId}");

                var order = new Order
                {
                    Id = userId, 
                    OrderDate = DateTime.Now,
                    TotalAmount = 0,
                    Status = "Pending",
                    OrderItems = new List<OrderItem>()
                };

                var response = await _httpClient.PostAsJsonAsync("https://localhost:7193/api/Orders", order);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Unable to create order. Status code: " + response.StatusCode);
                }

                var createdOrder = await response.Content.ReadFromJsonAsync<Order>();
                if (createdOrder == null)
                {
                    throw new Exception("Failed to deserialize the created order.");
                }

                var cartItem = new OrderItem
                {
                    OrderId = createdOrder.Id,
                    FoodItemId = item.Id,
                    Quantity = quantity,
                    Price = item.Price
                };

                var cartItemResponse = await _httpClient.PostAsJsonAsync("https://localhost:7193/api/OrderItems", cartItem);
                if (!cartItemResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Unable to add item to cart. Status code: " + cartItemResponse.StatusCode);
                }

                Console.WriteLine("Item added to cart successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
                throw new Exception("Unable to create order due to JSON deserialization error.");
            }
           
        }
    }

    }
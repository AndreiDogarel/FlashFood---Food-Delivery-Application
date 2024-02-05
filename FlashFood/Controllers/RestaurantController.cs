using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Management.Service.Models;
using User.Management.Service.Services.RestaurantService;

namespace FlashFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [Authorize]
        [HttpGet("search-restaurant")]
        public async Task<IActionResult> SearchRestaurantAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest("Keyword is required for searching restaurants.");
            }

            var search = await _restaurantService.SearchRestaurantsAsync(keyword);
            if (search == null || !search.Any())
            {
                return NotFound("No restaurants found matching your criteria.");
            }
            return Ok(search);
        }

        [Authorize]
        [HttpGet("get-restaurants")]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await _restaurantService.GetAllAsync();
            if(restaurants == null || !restaurants.Any())
            {
                return BadRequest("There isn't any restaurant available!");
            }
            return Ok(restaurants);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(Guid id)
        {
            var result = await _restaurantService.DeleteRestaurantAsync(id);
            if (!result)
            {
                return NotFound($"Restaurant with ID {id} not found.");
            }

            return Ok("The restaurant has been deleted!");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-restaurant")]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantModel model)
        {
            try
            {
                await _restaurantService.CreateRestaurantAsync(model);
                return Ok("Restaurant created successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while creating the restaurant: {ex.Message}");
            }
        }
    }
}

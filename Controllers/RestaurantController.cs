using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}

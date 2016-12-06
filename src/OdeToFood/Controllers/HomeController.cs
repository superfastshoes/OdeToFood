using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        // IActionResult is the formal way to encapsulate the decision of the controller
        public IActionResult Index()
        {
            // Instantiate model
            var model = _restaurantData.GetAll();
     
            return View(model);
        }
    }
}

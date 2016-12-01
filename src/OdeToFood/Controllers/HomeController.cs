using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        // IActionResult is the formal way to encapsulate the decision of the controller
        public IActionResult Index()
        {
            return Content("Hello, from the HomeController");
        }
    }
}

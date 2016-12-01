using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{

    // Alternative way to write the route [Attribute] - Great for special case routing that need additional parameters
    [Route("about")]
    public class AboutController
    {
        // Leaving the route name blank allows this to be the default route - "/about"
        [Route("")]
        public string Phone()
        {
            return "1+555-555-5555";
        }
        // "/about/address"
        [Route("address")]
        public string Address()
        {
            return "USA";
        }
    }
}

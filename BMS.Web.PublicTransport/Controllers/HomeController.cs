using Microsoft.AspNetCore.Mvc;

namespace BMS.Web.PublicTransport.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

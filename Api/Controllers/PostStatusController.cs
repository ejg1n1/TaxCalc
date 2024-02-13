using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class PostStatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

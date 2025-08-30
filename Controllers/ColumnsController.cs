using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers
{
    public class ColumnsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

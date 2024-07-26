using CW_W13_01_114.Data;
using CW_W13_01_114.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CW_W13_01_114.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var repo = new UserRepository();
            var users = repo.GetAll();
            var res = new List<IndexModel>();
            foreach (var user in users) { 
                res.Add(new IndexModel {Name = user.Name,Email = user.Email});
            }
            return View(res);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterPost(RegisterModel model)
        {
            var user = new User { Email = model.Email ,Password = model.Password,Name = model.Name};
            var repo = new UserRepository();
            repo.Create(user);
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

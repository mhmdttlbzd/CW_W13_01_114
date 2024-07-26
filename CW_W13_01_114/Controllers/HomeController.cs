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
                res.Add(new IndexModel { Id = user.Id.ToString(), Name = user.Name, Email = user.Email });
            }
            return View(res);
        }
        [HttpGet]
        public IActionResult Register(Guid id)
        {
            var repo = new UserRepository();
            var user = repo.GetById(id);
            var res = new RegisterModel();
            if (user != null) { res.Name = user.Name; res.Email = user.Email;res.Password = user.Password; }
            ViewData["id"] = id;
            return View(res);
        }

        [HttpPost]
        public IActionResult RegisterPost(RegisterModel model,Guid id)
        {
            var user = new User { Email = model.Email ,Password = model.Password,Name = model.Name};
            var repo = new UserRepository();
            if (id == Guid.Empty)
            {
                repo.Create(user);
            }
            else {
                repo.Update(user, id);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteUser(string id) {
            var repo = new UserRepository();
            repo.Delete(Guid.Parse(id));
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

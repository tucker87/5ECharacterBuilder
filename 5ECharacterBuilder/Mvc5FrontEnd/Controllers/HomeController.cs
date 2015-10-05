using Microsoft.AspNet.Mvc;
using _5ECharacterBuilder;
using static MVC5Library.AvailableClasses;
using static MVC5Library.AvailableRaces;
using static MVC5Library.AvailableBackgrounds;

namespace Mvc5FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var chracter = CharacterFactory.BuildACharacter(Human, Fighter, Acolyte);
            return View(chracter);
        }

        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}

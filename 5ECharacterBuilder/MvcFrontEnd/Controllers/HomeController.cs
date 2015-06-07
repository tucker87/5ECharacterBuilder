using System.Web.Mvc;
using _5ECharacterBuilder;
using static _5ECharacterBuilder.AvailableRaces;
using static _5ECharacterBuilder.AvailableClasses;
using static _5ECharacterBuilder.AvailableBackgrounds;

namespace MvcFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var character = CharacterFactory.BuildACharacter(Human, Fighter, Criminal);
            return View(character);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
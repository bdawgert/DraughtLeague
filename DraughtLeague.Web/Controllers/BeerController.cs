using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DraughtLeague.Web.Controllers
{
    public class BeerController : BaseController
    {
        public BeerController(SessionService sessionService) : base(sessionService) { }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

    }
}

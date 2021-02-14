using System;
using System.Linq;
using DraughtLeague.DAL.Models;
using DraughtLeague.Untappd.Utilities;
using DraughtLeague.Web.Validators;
using DraughtLeague.Web.ViewModels.Bar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DraughtLeague.Web.Controllers
{
    [Authorize]
    public class TapRoomController : BaseController
    {

        public TapRoomController(SessionService sessionService) : base(sessionService) { }

        public IActionResult Index() {

            IQueryable<TapRoom> bars = _dal.TapRooms.Where(x => x.UserId == _session.UserId);

            if (!bars.Any())
                return View("No bars found");

            return View("Yeah Bar!");
        }

        [HttpGet]
        [Route("TapRoom/Create")]
        [Route("TapRoom/Edit/{id}")]
        public IActionResult Create(Guid id) {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EditVM vm) {
            _dal.TapRooms.Add(new TapRoom {
                UserId = _session.UserId,
                Name = vm.Name,
            });

            return RedirectToAction("", "");
        }

        [HttpPost]
        public IActionResult Edit(EditVM vm) {
            TapRoom bar = _dal.TapRooms.Find(vm.Id);

            if (bar.UserId == _session.UserId) {
                bar.Name = vm.Name;
                _dal.SaveChanges();
            }

            return RedirectToAction("", "");
        }

        public IActionResult AddBeer(AddBeerVM vm) {
            TapRoom tapRoom = _dal.TapRooms.Where(x => x.Id == vm.TapRoomId).Include(x => x.Beers).Include(x => x.SeasonMember).ThenInclude(x => x.Season).FirstOrDefault();
            
            if (tapRoom.UserId != _session.UserId)
                return Json(new { ErrorMessage = "Unauthorized"});

            DataValidationResult validationResult = TapRoomValidator.AddBeerValidator(vm, tapRoom);
            if (!validationResult.IsValid)
                return Json(new {ErrorMessage = validationResult.ErrorMessages.First()});
            
            Beer beer = new Beer() {
                TapRoomId = vm.TapRoomId,
                BeerName = vm.BeerName,
                BreweryName = vm.BreweryName,
                StyleName = vm.StyleName,
                StyleFamily = vm.StyleFamily,
                UntappdId = vm.UntappdId
            };

            _dal.Beers.Add(beer);
            _dal.SaveChanges();

            return Json(new { });
        }




    }
}

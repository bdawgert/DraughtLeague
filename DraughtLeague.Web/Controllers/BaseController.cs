using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DraughtLeague.DAL;

namespace DraughtLeague.Web.Controllers
{
    public class BaseController : Controller
    {
        protected LeagueDbContext _dal;
        protected SessionService _session;

        public BaseController(SessionService sessionService) {
            _session = sessionService;
            _dal = sessionService.DAL;

        }

    }
}

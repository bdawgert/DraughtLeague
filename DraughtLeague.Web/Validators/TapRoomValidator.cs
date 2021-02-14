using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DraughtLeague.DAL.Models;
using DraughtLeague.Web.ViewModels.Bar;
using Microsoft.EntityFrameworkCore.Update;

namespace DraughtLeague.Web.Validators
{
    public static class TapRoomValidator
    {

        public static DataValidationResult AddBeerValidator(AddBeerVM vm, TapRoom tapRoom) {

            DataValidationResult result = new DataValidationResult();

            if (tapRoom == null)
                result.ErrorMessages.Add("Tap Room Not Found");
            else if (tapRoom.Beers.Count >= tapRoom.League.MaxRosterSize)
                result.ErrorMessages.Add("Your Keg Storage is Full");

            else if (DateTime.Now < tapRoom.Season?.WaiverStartDate && DateTime.Now > tapRoom.Season?.WaiverEndDate)
                result.ErrorMessages.Add("Waivers are not currently being accepted for the Season");

            result.IsValid = !result.ErrorMessages.Any();
            return result;


        }

    }
}

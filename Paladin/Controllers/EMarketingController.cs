using Paladin.Infrastructure;
using Paladin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace Paladin.Controllers
{
    [HttpAuthenticate("Marco","123")]
    public class EMarketingController : Controller
    {

        private PaladinDbContext context;

        public EMarketingController(PaladinDbContext context) {
            this.context = context;
        }

        [HttpPost]
        public ActionResult WeeklyReport(EWeeklyReport weeklyReport)
        {
            if (ModelState.IsValid) {
                context.WeeklyReports.Add(weeklyReport);
                context.SaveChanges();
                return Content("OK");
            }
            return Content("Error");
        }

        public ActionResult MonthlyReport(EMonthlyReport monthlyReport)
        {
            if (ModelState.IsValid) {
                context.MonthlyReports.Add(monthlyReport);
                context.SaveChanges();
                return Content("OK");
            }
            return Content("Error");
        }


    }
}
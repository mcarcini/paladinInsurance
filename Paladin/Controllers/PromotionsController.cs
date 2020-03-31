using Paladin.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paladin.Controllers
{
    public class PromotionsController : Controller
    {
        //Desktop
        public ActionResult Promos()
        {
            return View();
        }

        //mobile
        [IsPaladinMobile]
        public ActionResult GoPromos()
        {
            return View();
        }
    }
}
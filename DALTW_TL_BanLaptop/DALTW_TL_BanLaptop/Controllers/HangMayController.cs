using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DALTW_TL_BanLaptop.Models;
namespace DALTW_TL_BanLaptop.Controllers
{
    public class HangMayController : Controller
    {
        //
        // GET: /HangMay/
        DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();
        public ActionResult HangMayPartial()
        {
            var ListCD = db.HANGMAYs.Take(10).ToList();
            return View(ListCD);
        }
	}
}
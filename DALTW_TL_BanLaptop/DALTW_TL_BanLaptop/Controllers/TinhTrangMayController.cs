using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DALTW_TL_BanLaptop.Models;
namespace DALTW_TL_BanLaptop.Controllers
{
    public class TinhTrangMayController : Controller
    {
        //
        // GET: /TinhTrangMay/
        DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();
        public ActionResult TinhTrangMayPartial()
        {
            var ListCD = db.TINHTRANGMAYs.Take(10).ToList();
            return View(ListCD);
        }
	}
}
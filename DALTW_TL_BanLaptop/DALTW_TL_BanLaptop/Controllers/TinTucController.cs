using DALTW_TL_BanLaptop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DALTW_TL_BanLaptop.Models;
namespace DALTW_TL_BanLaptop.Controllers
{
    public class TinTucController : Controller
    {
        // GET: TinTuc
        DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TinTucPartial()
        {
            var lst = db.LOAITINs.OrderBy(x => x.TLTIN).ToList();
            return View(lst);
        }
        public ActionResult ListTT()
        {
            var lst = db.TINTUCs.ToList();
            return View(lst);
        }
        public ActionResult TTTheoLoai(int id)
        {
            var lst = db.TINTUCs.Where(x => x.MLTIN == id).ToList();
            LOAITIN t = db.LOAITINs.Where(x => x.MLTIN == id).FirstOrDefault();
            ViewBag.TenTT = t.TLTIN;
            return View(lst);
        }
    }
}
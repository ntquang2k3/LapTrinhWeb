using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DALTW_TL_BanLaptop.Models;
namespace DALTW_TL_BanLaptop.Controllers
{
    public class SanPhamController : Controller
    {
        //
        // GET: /Laptop/
        DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ShowAllSanPham(int page = 1)
        {
            List<LAPTOP> lst = db.LAPTOPs.OrderBy(t => t.TENLAP).ToList();

            //Paging
            int NoOfRecordPerPage = 6;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lst.Count()) /
                Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            lst = db.LAPTOPs.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(lst);
        }
        public ActionResult SearchSP(string txt_Search, int page = 1)
        {
            if (txt_Search != null)
            {
                Session["Search"] = txt_Search;
            }
            var lst = db.LAPTOPs.Where(x => x.TENLAP.Contains(Session["Search"].ToString())).ToList();
            ViewBag.Count = lst.Count;
            //Paging
            int NoOfRecordPerPage = 6;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lst.Count()) /
                Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            lst = db.LAPTOPs.Where(x => x.TENLAP.Contains(Session["Search"].ToString())).Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(lst);
        }
        public ActionResult XemChiTietSanPham(int maLap)
        {
            LAPTOP s = db.LAPTOPs.Where(x => x.MALAP == maLap).FirstOrDefault();
            return View(s);
        }
        public ActionResult SanPhamTheoHang(string MaHang, int page = 1)
        {          
            int mh = int.Parse(MaHang);
            List<LAPTOP> lst = db.LAPTOPs.Where(t => t.MAHANG == mh).ToList();
            ViewBag.MaHang = mh;
            if (lst.Count == 0)
            {
                ViewBag.TB = "Không tìm thấy";
            }
            //Paging
            int NoOfRecordPerPage = 6;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lst.Count()) /
                Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            lst = db.LAPTOPs.Where(t => t.MAHANG == mh).Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(lst);
        }
        public ActionResult SanPhamTheoTinhTrangMay(int MaTT, int page = 1)
        {
            ViewBag.MaTT = MaTT;
            var lst = db.LAPTOPs.Where(t => t.MATINHTRANG == MaTT).OrderBy(t => t.TENLAP).ToList();
            if (lst.Count == 0)
            {
                ViewBag.TB = "Không tìm thấy";
            }
            //Paging
            int NoOfRecordPerPage = 6;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lst.Count()) /
                Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            lst = db.LAPTOPs.Where(t => t.MATINHTRANG == MaTT).Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(lst);
        }
        public ActionResult donhangapi()
        {
            var lst = db.DONHANGs.ToList();
            return View(lst);
        }
        public ActionResult laptopapi()
        {
            var lst = db.LAPTOPs.ToList();
            return View(lst);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DALTW_TL_BanLaptop.Models;
namespace DALTW_TL_BanLaptop.Controllers
{
    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/
        DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lst = Session["GioHang"] as List<GioHang>;
            if (lst == null)
            {
                lst = new List<GioHang>();
                Session["GioHang"] = lst;
            }
            return lst;
        }
        public ActionResult ThemGioHang(int ms, string strURL)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang SanPham = lstGioHang.Find(sp => sp.iMaLap == ms);
            if (SanPham == null)
            {
                SanPham = new GioHang(ms);
                lstGioHang.Add(SanPham);
                return Redirect(strURL);
            }
            else
            {
                SanPham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lst = Session["GioHang"] as List<GioHang>;
            if (lst != null)
            {
                tsl = lst.Sum(x => x.iSoLuong);
            }
            return tsl;
        }
        private double TongThanhTien()
        {
            double ttt = 0;
            List<GioHang> lst = Session["GioHang"] as List<GioHang>;
            if (lst != null)
            {
                ttt += lst.Sum(x => x.dThanhTien);
            }
            return ttt;
        }
        public ActionResult GioHang()
        {
            var roleCookie = Request.Cookies["role"];
            if (roleCookie == null)
            {
                return RedirectToAction("Login", "User");
            }
            List<GioHang> lst = LayGioHang();
            if (Session["GioHang"] == null || lst.Count() == 0)
            {
                return RedirectToAction("GioHangTrong", "GioHang");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();
            return View(lst);
        }
        public ActionResult GioHangTrong()
        {
            return View();
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.SL = TongSoLuong();
            return View();
        }
        public ActionResult XoaGioHang(int MaSP)
        {
            List<GioHang> lstGioHang = LayGioHang();

            GioHang sp = lstGioHang.SingleOrDefault(s => s.iMaLap == MaSP);
            if (sp != null)
            {
                lstGioHang.RemoveAll(s => s.iMaLap == MaSP);
                return RedirectToAction("GioHang", "GioHang");
            }

            if (lstGioHang.Count == 0) 
            {
                return RedirectToAction("GioHangTrong", "GioHang");
            }

            return RedirectToAction("GioHang", "GioHang");
        }
        public ActionResult XoaGioHang_All()
        {
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("ShowAllSAnPham", "SanPham");
        }
        public ActionResult CapNhatGioHang(int MaSP, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Single(s => s.iMaLap == MaSP);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("GioHang", "GioHang");
        }
        [HttpGet]
        // Đặt hàng 
        public ActionResult DatHang()
        {
            KHACHHANG sa = Session["taikhoan"] as KHACHHANG;

            if (sa == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (Session["GioHang"] == null)
            {
                return RedirectToAction("ShowAllSanPham", "SanPham");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();

            return View(lstGioHang);

        }
        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            KHACHHANG sa = Session["taikhoan"] as KHACHHANG;

            if (sa == null)
            {
                return RedirectToAction("Login", "User");
            }
            // Thêm đơn hàng
            DONHANG ddh = new DONHANG();
            List<GioHang> gh = LayGioHang();
            ddh.MAKH = sa.MAKH;

            // Định dạng ngày theo đúng định dạng MM/dd/yyyy
            ddh.NGAYDAT = DateTime.Now;
            DateTime ngayGiao = new DateTime();
            if (DateTime.TryParseExact(f["NgayGiao"], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngayGiao))
            {
                ddh.NGAYGIAO = ngayGiao;
            }


            ddh.TINHTRANGGIAO = "Chưa Giao ";
            ddh.DATHANHTOAN = "Hoàn Tất";
            db.DONHANGs.InsertOnSubmit(ddh);
            db.SubmitChanges();

            // Thêm chi tiết đơn hàng
            foreach (var item in gh)
            {
                CHITIETDONHANG ctDH = new CHITIETDONHANG();
                ctDH.MADH = ddh.MADH;
                ctDH.MALAP = item.iMaLap;
                ctDH.SOLUONG = item.iSoLuong;
                ctDH.DONGIA = (float)item.dDonGia;
                db.CHITIETDONHANGs.InsertOnSubmit(ctDH);
            }

            db.SubmitChanges();
            Session["GioHang"] = null;
            
            return RedirectToAction("XacNhanDonHang", "GioHang");

        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}
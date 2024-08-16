using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DALTW_TL_BanLaptop.Models;
namespace DALTW_TL_BanLaptop.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();

        public ActionResult Index()
        {
            var roleCookie = Request.Cookies["role"];
            if (roleCookie == null)
            {
                return RedirectToAction("Login", "User");
            }
            else if (roleCookie.Value == "Admin")
            {
                DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();
                var listUser = db.KHACHHANGs.ToList();
                return View(listUser);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
            
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(KHACHHANG kh)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                bool coLoi = false;
                if (kh.TAIKHOAN == null)
                {
                    ModelState.AddModelError("TAIKHOAN", "Tài khoản là bắt buộc");
                    coLoi = true;
                }
                if (kh.MATKHAU == null)
                {
                    ModelState.AddModelError("MATKHAU", "Mật khẩu không được để trống");
                    coLoi = true;
                }
                if (coLoi)
                {
                    return View(); 
                }
            }              
            DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();
            KHACHHANG sa = db.KHACHHANGs.Where(t => t.TAIKHOAN == kh.TAIKHOAN && t.MATKHAU == kh.MATKHAU).FirstOrDefault();
            if (sa == null)
            {
                ModelState.AddModelError("TAIKHOAN", "Tài khoản hoặc mật khẩu không đúng");
                return View();
            }
            HttpCookie authCookie = new HttpCookie("auth", sa.HOTEN);
            HttpCookie roleCookie = new HttpCookie("role", sa.PHANQUYEN);

            Response.Cookies.Add(authCookie);
            Response.Cookies.Add(roleCookie);
            if (sa.PHANQUYEN.Contains("User"))
            {
                Session["CheckRole"] = "User";
            }
            else
            {
                Session["CheckRole"] = "Admin";
            }
            ViewBag.TB = "Đăng nhập thành công !!!";
            Session["taikhoan"] = sa;
            return RedirectToAction("ShowAllSanPham", "SanPham");
        }

        public ActionResult Logout()
        {
            Session["CheckRole"] = "User";
            HttpCookie authcookie = new HttpCookie("auth");
            authcookie.Expires = DateTime.Now.AddDays(-1);
            HttpCookie rolecookie = new HttpCookie("role");
            rolecookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(authcookie);
            Response.Cookies.Add(rolecookie);
            return RedirectToAction("ShowAllSanPham", "SanPham");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(KHACHHANG kh, string ConfirmPassWord)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                bool coLoi = false;
                if (kh.TAIKHOAN == null)
                {
                    ModelState.AddModelError("TAIKHOAN", "Tài khoản là bắt buộc");
                    coLoi = true;
                }
                else if (kh.TAIKHOAN.ToString().Length < 4 || kh.TAIKHOAN.ToString().Length > 15)
                {
                    ModelState.AddModelError("TAIKHOAN", "Tài khoản phải từ 4-15 kí tự");
                    coLoi = true;
                }
                if (kh.HOTEN == null)
                {
                    ModelState.AddModelError("HOTEN", "Họ tên là bắt buộc");
                    coLoi = true;
                }
                if (kh.MATKHAU == null)
                {
                    ModelState.AddModelError("MATKHAU", "Mật khẩu không được để trống");
                    coLoi = true;
                }
                if (ConfirmPassWord != kh.MATKHAU)
                {
                    ModelState.AddModelError("ConfirmPassWord", "Không khớp mật khẩu");
                    coLoi = true;
                }
                if (kh.DIENTHOAI == null)
                {
                    ModelState.AddModelError("DIENTHOAI", "Điện thoại là bắt buộc");
                    coLoi = true;
                }
                if (coLoi)
                {
                    return View(); 
                }
            }              
            DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();
            KHACHHANG sa = db.KHACHHANGs.Where(t => t.TAIKHOAN == kh.TAIKHOAN).FirstOrDefault();
            if (sa != null)
            {
                ModelState.AddModelError("TAIKHOAN", "Tài khoản đã tồn tại trong hệ thống");
                return View();
            }
            db.KHACHHANGs.InsertOnSubmit(kh);
            db.SubmitChanges();
            return RedirectToAction("Login", "User");
        }
        
        public ActionResult RemoveUser(int id)
        {
            DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();
            KHACHHANG tim = db.KHACHHANGs.Where(t => t.MAKH == id).FirstOrDefault();
            if (tim != null)
            {
                db.KHACHHANGs.DeleteOnSubmit(tim);
                db.SubmitChanges();
            }
            return RedirectToAction("Index", "User");
        }
        [HttpGet]
        public ActionResult EditUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditUser(int id, KHACHHANG kh)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                bool coLoi = false;
                if (kh.TAIKHOAN == null)
                {
                    ModelState.AddModelError("TAIKHOAN", "Tài khoản là bắt buộc");
                    coLoi = true;
                }
                else if (kh.TAIKHOAN.ToString().Length < 4 || kh.TAIKHOAN.ToString().Length > 15)
                {
                    ModelState.AddModelError("TAIKHOAN", "Tài khoản phải từ 4-15 kí tự");
                    coLoi = true;
                }
                if (kh.HOTEN == null)
                {
                    ModelState.AddModelError("HOTEN", "Họ tên là bắt buộc");
                    coLoi = true;
                }
                if (kh.DIENTHOAI == null)
                {
                    ModelState.AddModelError("DIENTHOAI", "Điện thoại là bắt buộc");
                    coLoi = true;
                }
                if (coLoi)
                {
                    return View();
                }
            }
            DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();
            KHACHHANG sa = db.KHACHHANGs.Where(t => t.MAKH == id).FirstOrDefault();
            if (sa != null)
            {
                sa.HOTEN = kh.HOTEN;
                sa.TAIKHOAN = kh.TAIKHOAN;
                sa.PHANQUYEN = kh.PHANQUYEN;
                sa.DIENTHOAI = kh.DIENTHOAI;
                sa.EMAIL = kh.EMAIL;
                db.SubmitChanges();
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("Login", "User");
        }
        public ActionResult AllLaptop(int page = 1)
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
        // tạo lap 
        public ActionResult CreateLaptop()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateLaptop(LAPTOP lap)
        {
            if (ModelState.IsValid)
            {
                
                if (!db.TINHTRANGMAYs.Any(t => t.MATINHTRANG == lap.MATINHTRANG))
                {
                    ModelState.AddModelError("MATINHTRANG", "Mã tình trạng không hợp lệ.");
                    return View(lap);
                }

                
                if (db.LAPTOPs.Any(d => d.TENLAP == lap.TENLAP))
                {
                    ViewBag.ThongBao = "Trùng mã";
                    return View();
                }
                else
                {
                    db.LAPTOPs.InsertOnSubmit(lap);
                    db.SubmitChanges();
                    return RedirectToAction("AllLaptop", "User");
                }
            }
            return View(lap);
        }
        
        // xóa
       
        public ActionResult RemoveLap(int id)
        {
           
            using (DB_BanLaptopDataContext db = new DB_BanLaptopDataContext())
            {
              
                var chiTietDonHangs = db.CHITIETDONHANGs.Where(c => c.MALAP == id).ToList();

                foreach (var chiTiet in chiTietDonHangs)
                {
                    db.CHITIETDONHANGs.DeleteOnSubmit(chiTiet);
                }

                LAPTOP lapToRemove = db.LAPTOPs.FirstOrDefault(t => t.MALAP == id);

                if (lapToRemove != null)
                {
                    db.LAPTOPs.DeleteOnSubmit(lapToRemove);
                    db.SubmitChanges();
                }
            }

            return RedirectToAction("AllLaptop", "User");
        }
        // details
        public ActionResult XemChiTietLap(int maLap)
        {
            LAPTOP s = db.LAPTOPs.Where(x => x.MALAP == maLap).FirstOrDefault();
            return View(s);
        }
        // cập nhật

        public ActionResult UpdateLap(int id = 0)
        {
            LAPTOP lap = db.LAPTOPs.SingleOrDefault(d => d.MALAP == id);
            if (lap == null)
            {
                return HttpNotFound();
            }
            return View(lap);
        }

        [HttpPost]
        public ActionResult UpdateLap(LAPTOP updatedLap)
        {
            if (ModelState.IsValid)
            {
                LAPTOP existingLap = db.LAPTOPs.SingleOrDefault(d => d.MALAP == updatedLap.MALAP);

                if (existingLap != null)
                {

                    existingLap.TENLAP = updatedLap.TENLAP;
                    //existingLap.TINHTRANGMAY = updatedLap.TINHTRANGMAY;
                    existingLap.MATINHTRANG = updatedLap.MATINHTRANG;
                    existingLap.GIABAN = updatedLap.GIABAN;
                    existingLap.MOTA = updatedLap.MOTA;
                    existingLap.NGAYCAPNHAT = updatedLap.NGAYCAPNHAT;
                    existingLap.GIABAN = updatedLap.GIABAN;
                    existingLap.ANHBIA = updatedLap.ANHBIA;
                    existingLap.SOLUONGTON = updatedLap.SOLUONGTON;
                    existingLap.GIABAN = updatedLap.GIABAN;
                    existingLap.MOI = updatedLap.MOI;

                    db.SubmitChanges();
                    return RedirectToAction("AllLaptop");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return View(updatedLap);
        
          }
   
        // xem đơn hàng
        public ActionResult AllDonHang(int page = 1)
        {
            var query = from donHang in db.DONHANGs
                join chiTietDonHang in db.CHITIETDONHANGs on donHang.MADH equals chiTietDonHang.MADH
                select new DonHangChiTietViewModel
                {
                    DonHang = donHang,
                    ChiTietDonHang = chiTietDonHang
                };
            int NoOfRecordPerPage = 6;


            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(query.Count()) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;

            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;

            var pagedQuery = query.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();

            return View(pagedQuery);
            //return View(query.ToList());
        }
        // khách hàng
        public ActionResult AllKhachHang(int page = 1)
        {

            List<KHACHHANG> lst = db.KHACHHANGs.OrderBy(t => t.HOTEN).ToList();
            
            //Paging
            int NoOfRecordPerPage = 6;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(lst.Count()) /
                Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.Page = page;
            ViewBag.NoOfPages = NoOfPages;
            lst = db.KHACHHANGs.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
            return View(lst);
        }
        // cập nhật khách hàng

        public ActionResult UpdateKH(int id = 0)
        {
            KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(d => d.MAKH == id);
            if (kh == null)
            {
                return HttpNotFound();
            }
            return View(kh);
        }

        [HttpPost]
        public ActionResult UpdateKH(KHACHHANG updatedkh)
        {
            if (ModelState.IsValid)
            {
                KHACHHANG existingLap = db.KHACHHANGs.SingleOrDefault(d => d.MAKH == updatedkh.MAKH);

                if (existingLap != null)
                {

                    existingLap.MAKH = updatedkh.MAKH;
                    existingLap.HOTEN = updatedkh.HOTEN;
                    existingLap.NGAYSINH = updatedkh.NGAYSINH;
                    existingLap.GIOITINH = updatedkh.GIOITINH;
                    existingLap.DIENTHOAI = updatedkh.DIENTHOAI;
                  
                    existingLap.TAIKHOAN = updatedkh.TAIKHOAN;
                    existingLap.MATKHAU = updatedkh.MATKHAU;
                    existingLap.EMAIL = updatedkh.EMAIL;
                    existingLap.DIACHI = updatedkh.DIACHI;
                    existingLap.PHANQUYEN = updatedkh.PHANQUYEN;

                    db.SubmitChanges();
                    return RedirectToAction("AllKhachHang");
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return View(updatedkh);
        }
        // xóa khách
        public ActionResult RemoveKH(int id)
        {
            using (DB_BanLaptopDataContext db = new DB_BanLaptopDataContext())
            {
                // Xóa đơn hàng liên quan
                var donhangs = db.DONHANGs.Where(d => d.MAKH == id).ToList();
                foreach (var donhang in donhangs)
                {
                    db.DONHANGs.DeleteOnSubmit(donhang);
                }
                db.SubmitChanges();

                // Xóa khách hàng
                var chiTietkh = db.KHACHHANGs.Where(c => c.MAKH == id).ToList();
                foreach (var chiTiet in chiTietkh)
                {
                    db.KHACHHANGs.DeleteOnSubmit(chiTiet);
                }

                db.SubmitChanges();
            }

            return RedirectToAction("AllKhachHang", "User");
        }


    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DALTW_TL_BanLaptop.Models;
namespace DALTW_TL_BanLaptop.Controllers
{
    public class AjaxSanPhamController : Controller
    {
        //
        // GET: /AjaxSanPham/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();
        //public JsonResult List()
        //{
        //    return Json(db.LAPTOPs.ToList(), JsonRequestBehavior.AllowGet);
        //}

        //public List<DonHangDTO> Get()
        //{

        //    try
        //    {
        //        using (DB_BanLaptopDataContext db = new DB_BanLaptopDataContext())
        //        {
        //            var donHangs = from dh in db.DONHANGs
        //                           select new DonHangDTO
        //                           {
        //                               MaDH = dh.MADH,
        //                               NgayGiao = (dh.NGAYGIAO != null && dh.NGAYGIAO >= SqlDateTime.MinValue.Value && dh.NGAYGIAO <= SqlDateTime.MaxValue.Value)
        //                                    ? dh.NGAYGIAO.Value
        //                                    : DateTime.MinValue,
        //                               NgayDat = (dh.NGAYDAT != null && dh.NGAYDAT >= SqlDateTime.MinValue.Value && dh.NGAYDAT <= SqlDateTime.MaxValue.Value)
        //                                    ? dh.NGAYDAT.Value
        //                                    : DateTime.MinValue,
        //                               DaThanhToan = dh.DATHANHTOAN,
        //                               TinhTrangGiao = dh.TINHTRANGGIAO,
        //                               MaKH = dh.MAKH ?? 0
        //                           };

        //            return donHangs.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Xử lý ngoại lệ (ví dụ: log hoặc throw lại)
        //        Console.WriteLine($"Lỗi: {ex.Message}");
        //        return null;
        //    }
        //}


        //public JsonResult donhangapi()
        //{
        //    //var lst = db.DONHANGs
        //    //    .Select(dh => new DonHangDTO
        //    //    {
        //    //        MaDH = dh.MADH,
        //    //        NgayGiao = dh.NGAYGIAO ?? DateTime.MinValue,
        //    //        NgayDat = dh.NGAYDAT ?? DateTime.MinValue,
        //    //        DaThanhToan = dh.DATHANHTOAN,
        //    //        TinhTrangGiao = dh.TINHTRANGGIAO,
        //    //        MaKH = dh.MAKH ?? 0
        //    //    })
        //    //    .ToList();

        //    //return Json(lst, JsonRequestBehavior.AllowGet);

        //}
        public JsonResult donhangapi()
        {
            try
            {
                using (DB_BanLaptopDataContext db = new DB_BanLaptopDataContext())
                {
                    var donHangs = from dh in db.DONHANGs
                                   select new DonHangDTO
                                   {
                                       MaDH = dh.MADH,
                                       NgayGiao = dh.NGAYGIAO ?? DateTime.MinValue,
                                       NgayDat = dh.NGAYDAT ?? DateTime.MinValue,
                                       DaThanhToan = dh.DATHANHTOAN,
                                       TinhTrangGiao = dh.TINHTRANGGIAO,
                                       MaKH = dh.MAKH ?? 0
                                   };

                    return Json(donHangs.ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ (ví dụ: log hoặc throw lại)
                Console.WriteLine("Lỗi: {ex.Message}");
                // Return an empty JsonResult or handle the exception as needed
                return Json(new List<DonHangDTO>(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}
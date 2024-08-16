using DALTW_TL_BanLaptop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DALTW_TL_BanLaptop.Controllers
{
    public class KhachHangApiController : ApiController
    {
        [HttpPost]
        public int InsertNewKhachHang(int makh, string TenKH, string ngaysinh, string gioitinh, int dienthoai, string taikhoan, string matkhau, string mail, string diachi)
        {
            try
            {
                DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();
                KHACHHANG s = new KHACHHANG();
                bool kt = db.KHACHHANGs.Any(kh => kh.TAIKHOAN.Equals(taikhoan));
                if (kt)
                {
                    return 0;
                }
                s.MAKH = makh;
                s.HOTEN = TenKH;
                s.NGAYSINH = DateTime.Parse(ngaysinh);
                s.GIOITINH = gioitinh;
                s.DIENTHOAI = dienthoai;
                s.TAIKHOAN = taikhoan;
                s.MATKHAU = matkhau;
                s.EMAIL = mail;
                s.DIACHI = diachi;
                db.KHACHHANGs.InsertOnSubmit(s);
                db.SubmitChanges();
                return 1;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        [HttpPut]
        public bool updateKH(int makh, string TenKH, string ngaysinh, string gioitinh, int dienthoai, string taikhoan, string matkhau, string mail, string diachi)
        {
            try
            {
                DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();
                KHACHHANG s = db.KHACHHANGs.FirstOrDefault(f => f.MAKH == makh);
                bool kt = db.KHACHHANGs.Any(kh => kh.TAIKHOAN.Equals(taikhoan));
                if (kt)
                {
                    return false;
                }
                s.MAKH = makh;
                s.HOTEN = TenKH;
                s.NGAYSINH = DateTime.Parse(ngaysinh);
                s.GIOITINH = gioitinh;
                s.DIENTHOAI = dienthoai;
                s.TAIKHOAN = taikhoan;
                s.MATKHAU = matkhau;
                s.EMAIL = mail;
                s.DIACHI = diachi;

                db.SubmitChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [HttpDelete]
        public bool Delete(int ma)
        {
            DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();
            KHACHHANG kh = db.KHACHHANGs.FirstOrDefault(s => s.MAKH == ma);

            if (kh != null) { return false; }
            db.KHACHHANGs.DeleteOnSubmit(kh);
            db.SubmitChanges();
            return true;
        }
    }
}

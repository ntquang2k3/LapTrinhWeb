using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DALTW_TL_BanLaptop.Models
{
    public class GioHang
    {
        DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();
        public int iMaLap { set; get; }
        public string sTenLap { set; get; }
        public string sAnh { set; get; }
        public double dDonGia { set; get; }
        public int iSoLuong { set; get; }
        public double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        public GioHang(int MaSP)
        {
            iMaLap = MaSP;
            LAPTOP sp = db.LAPTOPs.Single(s => s.MALAP == iMaLap);
            sTenLap = sp.TENLAP;
            sAnh = sp.ANHBIA;
            dDonGia = double.Parse(sp.GIABAN.ToString());
            iSoLuong = 1;
        }
    }
}
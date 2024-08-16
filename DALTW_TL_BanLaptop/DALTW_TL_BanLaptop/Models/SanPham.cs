using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DALTW_TL_BanLaptop.Models
{
    public class SanPham
    {
        public int MaLap { get; set; }
        public string TenLap { get; set; }
        public string TinhTrang { get; set; }
        public float GiaBan { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayCN { get; set; }
        public string Anh { get; set; }
        public int SLT { get; set; }
        public int MaCD { get; set; }
        public int MaSX { get; set; }
        public int MOI { get; set; }

        public SanPham()
        {
            
        }
    }
}
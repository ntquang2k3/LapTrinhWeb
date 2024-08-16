using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DALTW_TL_BanLaptop.Models
{
    public class DonHangDTO
    {
        public int MaDH { get; set; }
        public DateTime NgayGiao { get; set; }
        public DateTime NgayDat { get; set; }
        public string DaThanhToan { get; set; }
        public string TinhTrangGiao { get; set; }
        public int MaKH { get; set; }
        [JsonIgnore]
        public KhachHang KhachHang { get; set; }
        public DonHangDTO()
        {
            
        }
    }
}
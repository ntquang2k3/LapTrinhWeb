using DALTW_TL_BanLaptop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DALTW_TL_BanLaptop.Controllers
{
    public class SanPhamApiController : ApiController
    {
        DB_BanLaptopDataContext db = new DB_BanLaptopDataContext();

        public List<SanPhamDTO> Get()
        {
            var sanPhamList = db.LAPTOPs
                             .Select(x => new SanPhamDTO { Anh = x.ANHBIA, TenSP = x.TENLAP, maSP = x.MALAP.ToString() }).Take(12)
                             .ToList();
            return sanPhamList;

        }
    }
}

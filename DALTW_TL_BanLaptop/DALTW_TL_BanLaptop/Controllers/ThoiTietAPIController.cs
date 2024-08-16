using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DALTW_TL_BanLaptop.Models;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace DALTW_TL_BanLaptop.Controllers
{
    public class ThoiTietAPIController : Controller
    {
        //
        // GET: /ThoiTietAPI/
        public async Task<ActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&appid=b572031e394631d71995b9cc9259daa4&units=metric");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    ThoiTiet weatherData = JsonConvert.DeserializeObject<ThoiTiet>(json);
                    return View(weatherData);
                }
                else
                {
                    throw new Exception("Failed to fetch weather data");
                }
            }
        }
	}
}
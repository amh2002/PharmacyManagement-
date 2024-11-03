using Hope.Infrastructure.Base;
using Hope.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Hope.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task< IActionResult> Index()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5165/api/Dashboard/GetDashboardDetalis");

            string apiResponse = await response.Content.ReadAsStringAsync();



            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ViewBag.MedicineCount = JsonConvert.DeserializeObject<Hope.Infrastructure.DTO.DashboardDTO>(apiResponse).MedicineCount;
            ViewBag.SupplierCount = JsonConvert.DeserializeObject<Hope.Infrastructure.DTO.DashboardDTO>(apiResponse).SupplierCount;
            //ViewBag.MedicineCount = obj.MedicineCount;
            //ViewBag.SupplierCount = obj.SupplierCount;
            return View();

            }
            else
            {
                return RedirectToAction("ErrorPage", "Home");

            }



        }

        public IActionResult Privacy()
        {
            return View();
        }
		public IActionResult ErrorPage()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

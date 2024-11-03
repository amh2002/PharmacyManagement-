using Hope.Infrastructure.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Hope.UI.Controllers
{
    public class StoreController : BaseController
    {
        public async Task< IActionResult> Create()
        {
            HttpClient client = new HttpClient();

            var responseSupplier = await client.GetAsync("http://localhost:5165/api/Store/GetAllSupplier");

            string apiResponseSupplier = await responseSupplier.Content.ReadAsStringAsync();

            var responseMedicine = await client.GetAsync("http://localhost:5165/api/Medicine/GetAllMedicine");

            string apiResponseMedicine = await responseMedicine.Content.ReadAsStringAsync();




            if (responseSupplier.StatusCode == System.Net.HttpStatusCode.OK && responseMedicine.StatusCode == System.Net.HttpStatusCode.OK)
            {

                ViewBag.Supplier = JsonConvert.DeserializeObject<List<Hope.Infrastructure.DTO.SupplierDTO>>(apiResponseSupplier);
                ViewBag.Medicine = JsonConvert.DeserializeObject<List<Hope.Infrastructure.DTO.MedicineDTO>>(apiResponseMedicine);
                return View();
            }
            else
            {
                return RedirectToAction("ErrorPage", "Home");

            }
        }

        public async Task<IActionResult> AddNewStore(Hope.Infrastructure.DTO.StoreDTO storeDTO)
        {
            HttpClient client = new HttpClient();

            var ClientContextDTO = JsonConvert.SerializeObject(storeDTO);

            var response = await client.PostAsync("http://localhost:5165/api/Store/AddNewStore", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

            }
            else
            {

            }

            return View();

        }
    }
}

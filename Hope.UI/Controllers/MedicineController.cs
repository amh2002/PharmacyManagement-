using Hope.Infrastructure.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Hope.UI.Controllers
{
    public class MedicineController : BaseController
    {
        public async Task<IActionResult> Create()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5165/api/Medicine/GetAllMedicineDepartment");

            string apiResponse = await response.Content.ReadAsStringAsync();


           

           
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                 ViewBag.MedicineDepartment = JsonConvert.DeserializeObject<List<Hope.Infrastructure.DTO.MedicineDepartmentDTO>>(apiResponse);
                return View();
            }
            else
            {
                return RedirectToAction("ErrorPage", "Home");

            }

        }
        [HttpPost]
        public async Task<IActionResult> AddNewMedicine(Hope.Infrastructure.DTO.MedicineDTO medicineDTO)
        {

            if (medicineDTO.MedicineImage != null)
            {
                string fileName = Path.GetFileName(medicineDTO.MedicineImage.FileName);
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                string ReadPath = "http://localhost:50239/" + "images/" + fileName;
                using (var stream = new FileStream(uploadPath, FileMode.Create))
                {
                    medicineDTO.MedicineImage.CopyTo(stream);
                }

                medicineDTO.ImageFullPath = uploadPath;
                medicineDTO.ImageName = fileName;
                medicineDTO.ImageReadPath = ReadPath;

            }

            medicineDTO.MedicineImage = null;
            HttpClient client = new HttpClient();

            var ClientContextDTO = JsonConvert.SerializeObject(medicineDTO);

            var response = await client.PostAsync("http://localhost:5165/api/Medicine/AddNewMedicine", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

            }
            else
            {

            }

            return View();

        }

        public async Task<IActionResult> GetAllMedicines()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5165/api/Medicine/ShowAllMedicines");

            string apiResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<Hope.Infrastructure.DTO.MedicineDTO>>(apiResponse);

            return View(result);
        }
    }
}

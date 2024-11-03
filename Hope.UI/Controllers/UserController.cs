using Hope.Infrastructure.Base;
using Hope.Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Hope.UI.Controllers
{
    public class UserController : BaseController
    {
        public async Task <IActionResult> Create()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5165/api/User/GetAllJobDescriptions");

            string apiResponse = await response.Content.ReadAsStringAsync();

            

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				ViewBag.JobDescription = JsonConvert.DeserializeObject<List<Hope.Infrastructure.DTO.JobDescriptionDTO>>(apiResponse);

				return View();

			}
			else
			{
                return RedirectToAction("ErrorPage", "Home");

			}

		}

        public async Task<IActionResult> AddNewUser(Hope.Infrastructure.DTO.UserDTO userDTO)
        {
            HttpClient client = new HttpClient();

            var ClientContextDTO = JsonConvert.SerializeObject(userDTO);

            var response = await client.PostAsync("http://localhost:5165/api/User/AddNewUser", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllUsers", "User");
            }
            else
            {
                return RedirectToAction("ErrorPage", "Home");
            }

            

        }

        public async Task<IActionResult> GetAllUsers()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5165/api/User/GetAllUsers");

            string apiResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<Hope.Infrastructure.DTO.UserDTO>>(apiResponse);

            return View(result);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync("http://localhost:5165/api/User/DeleteUser?id=" + Id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("GetAllUsers");
            }
            else
            {
                return View();
            }

            
        }

        public async Task<IActionResult> Update(int Id)
        {
            HttpClient client = new HttpClient();

           

            var response = await client.GetAsync("http://localhost:5165/api/User/GetUserById?id=" + Id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var userDTO = JsonConvert.DeserializeObject<Hope.Infrastructure.DTO.UserDTO>(apiResponse);




            //var responseJob = await client.GetAsync("http://localhost:5165/api/User/GetAllJobDescriptions");
            //string apiResponseJob = await responseJob.Content.ReadAsStringAsync();
            //ViewBag.JobDescription = JsonConvert.DeserializeObject<List<Hope.Infrastructure.DTO.JobDescriptionDTO>>(apiResponseJob);

            ViewBag.JobDescription = userDTO.JobList;


            return View(userDTO);

 

        }
        public async Task<IActionResult> UpdateUser(Hope.Infrastructure.DTO.UserDTO userDTO)
        {
            HttpClient client = new HttpClient();


            var ClientContextDTO = JsonConvert.SerializeObject(userDTO);


            var response = await client.PostAsync("http://localhost:5165/api/User/UpdateUser", new StringContent(ClientContextDTO, Encoding.UTF8, "application/json"));

            
            return RedirectToAction("GetAllUsers");

            
        }


    }


}


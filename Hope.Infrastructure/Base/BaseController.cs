using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Infrastructure.Base
{
    public class BaseController : Controller
    {
        public override async void OnActionExecuting(ActionExecutingContext context)
        {

            if (IsUserLoggedIn())
            {
                ViewBag.UserId = HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault().Value;
            }
            else 
            {

              context.Result = RedirectToAction("Login", "Account");
               

            }

        }

        
    
    public bool IsUserLoggedIn()
        {
            var result = HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault();

            if (result == null) return false;
            else return true;
            
        }

        public async Task<IActionResult> PermissionByUserId()
        {
            int id = 0;

            if(ViewBag.UserId != null)
            
                id = Convert.ToInt32(ViewBag.UserId);

                HttpClient httpClient = new HttpClient();

                var response = await httpClient.GetAsync("http://localhost:5165/api/Common/PermissionByUserId?id=" + id);

                Infrastructure.DTO.NavbarPermissionDTO navbarPermissionDTO = new Infrastructure.DTO.NavbarPermissionDTO();


                var Data = await response.Content.ReadAsStringAsync();

                navbarPermissionDTO = JsonConvert.DeserializeObject<DTO.NavbarPermissionDTO>(Data);

                return PartialView("_ManageNavbar", navbarPermissionDTO);
            
        }
    }
}

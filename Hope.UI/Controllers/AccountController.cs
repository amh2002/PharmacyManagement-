using Hope.Infrastructure.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace Hope.UI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public async Task <IActionResult> LoginUser(Infrastructure.DTO.LoginDTO loginDTO)
        {
            var Item = CheckLoginUser(loginDTO).Result;

            if (Item == -1)
            {
                return View("Login");
                //return RedirectToAction("Index", "Home");

            }
            else
            {
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier , loginDTO.UserName),
                    new Claim("UserName" , loginDTO.UserName),
                    new Claim("UserId", Item.ToString())
                };

                var userIdentity = new ClaimsIdentity(userClaims,"User Identity");

                var userPrincipal = new ClaimsPrincipal(new [] { userIdentity });

                _ = HttpContext.SignInAsync(userPrincipal);

                return RedirectToAction("Index", "Home");
            }


            }

        public async Task<int> CheckLoginUser(Infrastructure.DTO.LoginDTO loginDTO)
        {
            HttpClient client = new HttpClient();

            var LoginContextDTO = JsonConvert.SerializeObject(loginDTO);

            var response = await client.PostAsync("http://localhost:5165/api/User/Login", 
            new StringContent(LoginContextDTO, Encoding.UTF8, "application/json"));

            var Data = await response.Content.ReadAsStringAsync();
            int Id = JsonConvert.DeserializeObject<int>(Data);

            return Id;
        }

        public async Task<IActionResult> LogOut()
        {
            var _user = HttpContext.User as ClaimsPrincipal;
            var _identity = _user.Identity as ClaimsIdentity;

            foreach (var claim in _user.Claims.ToList())
            {
                _identity.RemoveClaim(claim);
            }

            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}

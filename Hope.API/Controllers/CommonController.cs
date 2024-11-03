using Hope.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hope.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CommonController : Controller
    {
        private readonly IAssignUsersToRoleRepository _assignUsersToRoleRepository;

        private readonly IModuleRoleRepository _moduleRoleRepository;

        public CommonController(IAssignUsersToRoleRepository assignUsersToRoleRepository, IModuleRoleRepository moduleRoleRepository)
        {
            _assignUsersToRoleRepository = assignUsersToRoleRepository;
            _moduleRoleRepository = moduleRoleRepository;
        }

        public IActionResult PermissionByUserId(int id)
        {
            List<int> lstRoles = _assignUsersToRoleRepository.Find(obj => obj.UserId== id).Select(obj => obj.RoleId).ToList();

            List<int> lstModules = _moduleRoleRepository.Find(x => lstRoles.Contains(x.RoleId)).Select(x => x.ModuleId).Distinct().ToList();

            Infrastructure.DTO.NavbarPermissionDTO navbarPermissionDTO = new Infrastructure.DTO.NavbarPermissionDTO();

            if (lstModules.Contains(1))
                navbarPermissionDTO.User = "true";
            if (lstModules.Contains(2))
                navbarPermissionDTO.Medicine = "true";
            if (lstModules.Contains(3))
                navbarPermissionDTO.Store = "true";


            string jsonString = JsonConvert.SerializeObject(navbarPermissionDTO, Formatting.None, new JsonSerializerSettings
            {

                ReferenceLoopHandling = ReferenceLoopHandling.Ignore

            });
            return Ok(jsonString);
        }
    }
}

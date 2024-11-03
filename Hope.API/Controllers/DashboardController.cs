using Hope.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hope.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ISupplierRepository _supplierRepository;

        public DashboardController(IStoreRepository storeRepository, ISupplierRepository supplierRepository)
        {
            _storeRepository = storeRepository;
            _supplierRepository = supplierRepository;
        }

        public IActionResult GetDashboardDetalis()
        {
            Infrastructure.DTO.DashboardDTO dashboardDTO = new Infrastructure.DTO.DashboardDTO();
            dashboardDTO.MedicineCount = _storeRepository.Find(x => x.ExpiryDate < DateTime.Today.AddDays(30) && x.ExpiryDate > DateTime.Now).Count();
            dashboardDTO.SupplierCount = _supplierRepository.GetAll().Count();

            string jsonString = JsonConvert.SerializeObject(dashboardDTO, Formatting.None, new JsonSerializerSettings
            {

                ReferenceLoopHandling = ReferenceLoopHandling.Ignore

            });

            return Ok(jsonString);
        }
    }
}

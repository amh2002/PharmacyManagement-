using Hope.Repository.IRepository;
using Hope.Repository.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Hope.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StoreController : Controller
    {


        private readonly IStoreRepository _storeRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IErrorLogRepository _erorrLogRepository;

        public StoreController(IStoreRepository storeRepository, ISupplierRepository supplierRepository, IErrorLogRepository erorrLogRepository)
        {
            _storeRepository = storeRepository;
            _supplierRepository = supplierRepository;
            _erorrLogRepository = erorrLogRepository;
        }

        public IActionResult AddNewStore(Hope.Infrastructure.DTO.StoreDTO storeDTO)
        {
            try
            {
                EntityComponent.DBEntities.Store obj = new EntityComponent.DBEntities.Store();

                obj.MedicineId = storeDTO.MedicineId;
                obj.SupplierId = storeDTO.SupplierId;
                obj.OrginalQty = storeDTO.OrginalQty;
                obj.RemaningQty = storeDTO.RemaningQty;
                obj.CostPrice = storeDTO.CostPrice;
                obj.TaxValue = storeDTO.TaxValue;
                obj.SellingPriceBeforeTax = storeDTO.SellingPriceBeforeTax;
                obj.SellingPriceAfterTax = storeDTO.SellingPriceAfterTax;
                obj.MaxDiscount = storeDTO.MaxDiscount;
                obj.ProductionDate = storeDTO.ProductionDate;
                obj.ExpiryDate = storeDTO.ExpiryDate;

                _storeRepository.Add(obj);
                return Ok("Success");

            }
            catch (Exception)
            {
                return Ok("Fail");
            }


        }


        public IActionResult GetAllSupplier()
        {
            try
            {
                List<Hope.Infrastructure.DTO.SupplierDTO> lst = new List<Infrastructure.DTO.SupplierDTO>();

                lst = (from obj in _supplierRepository.GetAll()
                       select new Hope.Infrastructure.DTO.SupplierDTO
                       {
                           SupplierId = obj.SupplierId,
                           SupplierName = obj.SupplierName,
                       }).ToList();

                string jsonString = JsonConvert.SerializeObject(lst, Formatting.None, new JsonSerializerSettings
                {

                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore

                });

                return Ok(jsonString);

            }
            catch (Exception ex)
            {

                try
                {
                    EntityComponent.DBEntities.ErrorLog obj = new EntityComponent.DBEntities.ErrorLog();
                    obj.ErrorException = ex.InnerException != null ? ex.InnerException.ToString() : "";
                    obj.ErrorMessage = ex.Message != null ? ex.Message.ToString() : "";
                    obj.ModuleName = "User - GetAllJobDescriptions";
                    obj.TransactionDate = DateTime.Now;
                    _erorrLogRepository.Add(obj);
                }
                catch (Exception e)
                {
                    var appLog = new EventLog("Application");
                    appLog.Source = "Application";
                    appLog.WriteEntry(e.Message, EventLogEntryType.Error);
                }


                return BadRequest(ex.Message);
            }
        }
    }
}

using Hope.Repository.IRepository;
using Hope.Repository.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Hope.API.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MedicineController : Controller
    {

        private readonly IMedicineRepository _medicineRepository;
        private readonly IMedicineDepartmentRepository _medicineDepartmentRepository;
        private readonly IErrorLogRepository _erorrLogRepository;

        public MedicineController(IMedicineRepository medicineRepository, IMedicineDepartmentRepository medicineDepartmentRepository, IErrorLogRepository erorrLogRepository)
        {
            _medicineRepository = medicineRepository;

            _medicineDepartmentRepository = medicineDepartmentRepository;
            _erorrLogRepository = erorrLogRepository;
        }


        public IActionResult AddNewMedicine(Hope.Infrastructure.DTO.MedicineDTO medicineDTO)
        {
            try
            {
                EntityComponent.DBEntities.Medicine obj = new EntityComponent.DBEntities.Medicine();

                obj.MedicineName = medicineDTO.MedicineName;
                obj.MedicineDepartmentId = medicineDTO.MedicineDepartmentId;
                obj.Description = medicineDTO.Description;


                _medicineRepository.Add(obj);
                return Ok("Success");

            }
            catch (Exception)
            {
                return Ok("Fail");
            }


        }


        public IActionResult GetAllMedicineDepartment()
        {
            try
            {
                List<Hope.Infrastructure.DTO.MedicineDepartmentDTO> lst = new List<Infrastructure.DTO.MedicineDepartmentDTO>();

                lst = (from obj in _medicineDepartmentRepository.GetAll()
                       select new Hope.Infrastructure.DTO.MedicineDepartmentDTO
                       {
                           Id = obj.MedicineDepartmentId,
                           Name = obj.DepartmentName,
                       }).ToList();

                string jsonString = JsonConvert.SerializeObject(lst, Formatting.None, new JsonSerializerSettings
                {

                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore

                });

                return Ok(jsonString);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        public IActionResult GetAllMedicine()
        {
            try
            {
                List<Hope.Infrastructure.DTO.MedicineDTO> lst = new List<Infrastructure.DTO.MedicineDTO>();

                lst = (from obj in _medicineRepository.GetAll()
                       select new Hope.Infrastructure.DTO.MedicineDTO
                       {
                           MedicineId = obj.MedicineId,
                           MedicineName = obj.MedicineName,
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

        public IActionResult ShowAllMedicines()
        {
            List<Infrastructure.DTO.MedicineDTO> lst = new List<Infrastructure.DTO.MedicineDTO>();

            lst = (from obj in _medicineRepository.Find(x => x.MedicineId != 0, x => x.MedicineDepartment)
                   select new Infrastructure.DTO.MedicineDTO
                   {
                       MedicineName = obj.MedicineName,
                       DepartmentName = obj.MedicineDepartment.DepartmentName,
                       Description = obj.Description,


                   }).ToList();

            string jsonString = JsonConvert.SerializeObject(lst, Formatting.None, new JsonSerializerSettings
            {

                ReferenceLoopHandling = ReferenceLoopHandling.Ignore

            });
            return Ok(jsonString);

        }
    }
}

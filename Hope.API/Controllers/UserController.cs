using Hope.Infrastructure.DTO;
using Hope.Repository.IRepository;
using Hope.Repository.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Hope.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly IJobDescriptionRepository _jobDescriptionRepository;
        private readonly IErrorLogRepository _erorrLogRepository;

        public UserController(IUserRepository userRepository, IJobDescriptionRepository jobDescriptionRepository, IErrorLogRepository errorLogRepository)
        {
            _userRepository = userRepository;
            _jobDescriptionRepository = jobDescriptionRepository;
            _erorrLogRepository = errorLogRepository;
        }

        public IActionResult GetAllUsers()
        {
            List<Infrastructure.DTO.UserDTO> lst = new List<Infrastructure.DTO.UserDTO>();

            lst = (from obj in _userRepository.Find(x => x.UserId != 0, x => x.JobDescription)
                   select new Infrastructure.DTO.UserDTO
                   {
                       UserId = obj.UserId,
                       FirstName = obj.FirstName,
                       LastName = obj.LastName,
                       Email = obj.Email,
                       Address = obj.Address,
                       DateOfBirth = obj.DateOfBirth,
                       GenderDisplayName = obj.Gender == true ? "Male" : "Female",
                       JoinDate = obj.JoinDate,
                       MobileNumber = obj.MobileNumber,
                       Salary = obj.Salary,
                       ShiftTypeName = obj.ShiftType == false ? "Shift A" : "Shift B",
                       JobDescriptionName = obj.JobDescription.Name,
                       IsActive = obj.IsActive,


                   }).ToList();




            string jsonString = JsonConvert.SerializeObject(lst, Formatting.None, new JsonSerializerSettings
            {

                ReferenceLoopHandling = ReferenceLoopHandling.Ignore

            });
            return Ok(jsonString);
        }

        public IActionResult GetUserById(int id)
        {
            Infrastructure.DTO.UserDTO userDTO = new Infrastructure.DTO.UserDTO();

            userDTO = (from obj in _userRepository.Find(x => x.UserId == id)
                       select new Infrastructure.DTO.UserDTO
                       {
                           UserId = obj.UserId,
                           JobDescriptionId = obj.JobDescriptionId,
                           FirstName = obj.FirstName,
                           LastName = obj.LastName,
                           Email = obj.Email,
                           Address = obj.Address,
                           DateOfBirth = obj.DateOfBirth,
                           JoinDate = obj.JoinDate,
                           MobileNumber = obj.MobileNumber,
                           Salary = obj.Salary,
                           Gender = obj.Gender,
                           ShiftType = obj.ShiftType,



                       }).FirstOrDefault();


            userDTO.JobList = new List<Infrastructure.DTO.JobDescriptionDTO>();

            userDTO.JobList = (from obj in _jobDescriptionRepository.GetAll()
                               select new Hope.Infrastructure.DTO.JobDescriptionDTO
                               {
                                   Id = obj.JobDescriptionId,
                                   Name = obj.Name,
                               }).ToList();

            string jsonString = JsonConvert.SerializeObject(userDTO, Formatting.None, new JsonSerializerSettings
            {

                ReferenceLoopHandling = ReferenceLoopHandling.Ignore

            });
            return Ok(jsonString);

        }

        public IActionResult AddNewUser(Hope.Infrastructure.DTO.UserDTO userDTO)
        {
            try
            {
                EntityComponent.DBEntities.User obj = new EntityComponent.DBEntities.User();

                obj.FirstName = userDTO.FirstName;
                obj.LastName = userDTO.LastName;
                obj.MobileNumber = userDTO.MobileNumber;
                obj.Address = userDTO.Address;
                obj.Gender = userDTO.Gender;
                obj.DateOfBirth = userDTO.DateOfBirth;
                obj.ShiftType = userDTO.ShiftType;
                obj.JoinDate = userDTO.JoinDate;
                obj.JobDescriptionId = userDTO.JobDescriptionId;
                obj.Email = userDTO.Email;
                obj.Salary = userDTO.Salary;
                obj.IsActive = true;
                obj.UserName = userDTO.UserName;
                obj.Password = userDTO.Password;
                   

                _userRepository.Add(obj);
                return Ok("Success");

            }
            catch (Exception)
            {
                return Ok("Fail");
            }


        }

        public IActionResult UpdateUser(Infrastructure.DTO.UserDTO userDTO)
        {

            EntityComponent.DBEntities.User obj = new EntityComponent.DBEntities.User();


            obj = _userRepository.Find(x => x.UserId == userDTO.UserId).FirstOrDefault();

            obj.FirstName = userDTO.FirstName;
            obj.LastName = userDTO.LastName;
            obj.MobileNumber = userDTO.MobileNumber;
            obj.Address = userDTO.Address;
            obj.Gender = userDTO.Gender;
            obj.DateOfBirth = userDTO.DateOfBirth;
            obj.ShiftType = userDTO.ShiftType;
            obj.JoinDate = userDTO.JoinDate;
            obj.JobDescriptionId = userDTO.JobDescriptionId;
            obj.Email = userDTO.Email;
            obj.Salary = userDTO.Salary;
            obj.IsActive = true;
            _userRepository.Update(obj);
            return Ok("Success");


        }

        public IActionResult GetAllJobDescriptions()
        {

            try
            {
                List<Hope.Infrastructure.DTO.JobDescriptionDTO> lst = new List<Infrastructure.DTO.JobDescriptionDTO>();

                lst = (from obj in _jobDescriptionRepository.GetAll()
                       select new Hope.Infrastructure.DTO.JobDescriptionDTO
                       {
                           Id = obj.JobDescriptionId,
                           Name = obj.Name,
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

        public IActionResult DeleteUser(int Id)
        {
            try
            {
                var obj = new EntityComponent.DBEntities.User();

                obj = _userRepository.Find(x => x.UserId == Id).FirstOrDefault();

                _userRepository.Delete(obj);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                EntityComponent.DBEntities.ErrorLog obj = new EntityComponent.DBEntities.ErrorLog();
                obj.ErrorException = ex.InnerException != null ? ex.InnerException.ToString() : "";
                obj.ErrorMessage = ex.Message != null ? ex.Message.ToString() : "";
                obj.ModuleName = "User - DeleteUser";
                obj.TransactionDate = DateTime.Now;
                _erorrLogRepository.Add(obj);
                return BadRequest();
            }
        }

        public IActionResult Login(Infrastructure.DTO.LoginDTO loginDTO)
        {
            var Result = _userRepository.Find(x => x.UserName == loginDTO.UserName
            && x.Password == loginDTO.Password).FirstOrDefault();

            if (Result != null)
            {
                if (Result.Password == loginDTO.Password)
                {
                    return Ok(Result.UserId);
                }
                else return BadRequest(-1);
            }
            else
            {

                return BadRequest(-1);


            }
        }
    }
}

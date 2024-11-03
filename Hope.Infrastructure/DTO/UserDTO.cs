using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Infrastructure.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public string FirstName { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public string LastName { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]

        public string Email { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public string MobileNumber { get; set; } = null!;


        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public bool Gender { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public string Address { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public bool ShiftType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public decimal Salary { get; set; }

        public int JobDescriptionId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public DateTime JoinDate { get; set; }

        //public DateOnly? ResignationDate { get; set; }

        public bool IsActive { get; set; }

        public string GenderDisplayName { get; set; }

        public string JobDescriptionName { get; set; }

        public string ShiftTypeName { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public List<JobDescriptionDTO> JobList { get; set; }


    }
}

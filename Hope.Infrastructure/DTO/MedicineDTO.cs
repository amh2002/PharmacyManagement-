using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Infrastructure.DTO
{
    public class MedicineDTO
    {

        public int MedicineId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]

        public string MedicineName { get; set; } = null!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]

        public int MedicineDepartmentId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]

        public string Description { get; set; } = null!;

        public string DepartmentName { get; set; }

        public IFormFile MedicineImage { get; set; }
        public string ImageFullPath { get; set; }
        public string ImageName { get; set; }
        public string ImageReadPath { get; set; }
    }
}

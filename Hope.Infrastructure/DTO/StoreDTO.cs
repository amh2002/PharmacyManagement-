using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Infrastructure.DTO
{
    public class StoreDTO
    {
        public int StoreId { get; set; }

        public int SupplierId { get; set; }

        public int MedicineId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public int OrginalQty { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public int RemaningQty { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public decimal CostPrice { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public decimal TaxValue { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public decimal SellingPriceBeforeTax { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public decimal SellingPriceAfterTax { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public decimal MaxDiscount { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public DateTime ProductionDate { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = "This filed is required")]
        public DateTime ExpiryDate { get; set; }
    }
}

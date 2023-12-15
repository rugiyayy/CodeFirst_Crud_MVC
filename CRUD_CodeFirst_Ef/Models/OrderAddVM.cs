using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRUD_CodeFirst_Ef.Models
{
    public class OrderAddVM
    {
        [ValidateNever]
        [BindProperty]
        public List<SelectListItem> Products { get; set; }

        [ValidateNever]
        [BindProperty]
        public List<SelectListItem> Customers { get; set; }


        [BindProperty]
        [Required(ErrorMessage = "Order Date is required!")]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [BindProperty]
        public int ProductId { get; set; }

        [BindProperty]
        public int CustomerId { get; set; }

        
       



    }
}

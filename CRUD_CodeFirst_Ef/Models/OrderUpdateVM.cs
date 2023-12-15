using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRUD_CodeFirst_Ef.Models
{
    public class OrderUpdateVM
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Order Date is required!")]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ValidateNever]
        [BindProperty]
        public List<SelectListItem> Products { get; set; }

        [ValidateNever]
        [BindProperty]
        public List<SelectListItem> Customers { get; set; }
    }
}

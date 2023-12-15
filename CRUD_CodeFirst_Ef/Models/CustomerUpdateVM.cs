using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRUD_CodeFirst_Ef.Models
{
    public class CustomerUpdateVM
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required!")]
        [MinLength(5, ErrorMessage = "Product Name can't be less than 3 characters!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Product Name is required!")]
        [MinLength(5, ErrorMessage = "Product Name can't be less than 3 characters!")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "The email address is required!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }


    }
}

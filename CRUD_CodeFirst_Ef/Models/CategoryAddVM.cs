using System.ComponentModel.DataAnnotations;

namespace CRUD_CodeFirst_Ef.Models
{
    public class CategoryAddVM
    {

        [Required(ErrorMessage = "Category Name is required!")]
        [MinLength(3, ErrorMessage = "Category Name can't be less than 3 characters!")]
        public string Name { get; set; }
    }
}

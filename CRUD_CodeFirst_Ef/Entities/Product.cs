using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_CodeFirst_Ef.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //[Column(TypeName ="decimal(5,2")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; } // FK


        // navigation properties
        public ProductImage? ProductImage { get; set; }
        public List<Order> Orders { get; set; }
        public Category Category { get; set; }


    }
}

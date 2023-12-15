namespace CRUD_CodeFirst_Ef.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ProductImageName { get; set; }
        
        
        public int ProductId { get; set; } //foreign key 
        public Product Product { get; set; }  // navigation properties
    }
}

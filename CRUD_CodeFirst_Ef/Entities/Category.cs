namespace CRUD_CodeFirst_Ef.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }


        // navigation properties
        public List<Product> Products { get; set; }

    }
}

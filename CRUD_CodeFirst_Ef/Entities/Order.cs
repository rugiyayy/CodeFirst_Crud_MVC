namespace CRUD_CodeFirst_Ef.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public int CustomerId { get; set; }


        // navigation properties
        public Product Product { get; set; }
        public Customer Customer { get; set; }


    }
}

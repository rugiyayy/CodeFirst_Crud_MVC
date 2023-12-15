namespace CRUD_CodeFirst_Ef.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // navigation properties
        public List<Order> Orders { get; set; }
    }
}

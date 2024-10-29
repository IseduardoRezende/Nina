namespace Nina.Tests.Models
{
    internal class Product
    {
        public Guid Code { get; set; }

        public decimal Value { get; set; }

        public ICollection<Product> DerivedProducts { get; set; }
    }
}

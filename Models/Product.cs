namespace ProductsApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public bool InStock { get; set; } = false;
        public required string Name { get; set; }
    }
}

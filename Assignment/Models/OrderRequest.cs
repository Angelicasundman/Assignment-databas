namespace Assignment.Models
{
    public class OrderRequest
    {
        public decimal TotalPrice { get; set; }
        public string CustomerName { get; set; } = null!;
        public int CustomersId { get; set; }
        public ICollection<ProductModel> Products { get; set; } = null!;
    }
}

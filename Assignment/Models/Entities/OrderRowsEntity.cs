namespace Assignment.Models.Entities
{
    public class OrderRowsEntity
    {
        public int Id { get; set; }
      public int? OrderId { get; set; }
        public int ProductId { get; set; }
        public OrderEntity? Order { get; set; }
        public ProductEntity? Product { get; set; }
        public ICollection<ProductEntity>? Produts { get; set; }
    }
}

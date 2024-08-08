namespace inventory.domain.Core
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int InventoryCount { get; set; }
        public decimal Price { get; set; }
        public float Discount { get; set; } // percentage
    }
}

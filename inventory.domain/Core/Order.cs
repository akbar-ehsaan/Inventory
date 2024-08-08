namespace inventory.domain.Core
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Product Product { get; private set; }
        public User Buyer { get; private set; }
        public DateTime CreationDate { get; private set; }
        public int Quantity { get; private set; }

        public Order(Product product, User buyer, int quantity)
        {
            Id = Guid.NewGuid();
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Buyer = buyer ?? throw new ArgumentNullException(nameof(buyer));
            CreationDate = DateTime.UtcNow;
            Quantity = quantity > 0 ? quantity : throw new ArgumentException("Quantity must be greater than zero.");
        }

        public decimal GetTotalPrice()
        {
            return Product.GetPriceWithDiscount() * Quantity;
        }
    }
}

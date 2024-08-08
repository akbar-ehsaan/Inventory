namespace inventory.domain.Core
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        private readonly List<Order> _orders = new List<Order>();
        public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();
        public User()
        {
                
        }

        public User(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");

            Id = Guid.NewGuid();
            Name = name;
        }

        public void PlaceOrder(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentException("Product cannot be null.");
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            if (product.InventoryCount < quantity)
                throw new InvalidOperationException("Insufficient inventory to fulfill order.");

            var order = new Order(product, this, quantity);
            _orders.Add(order);
            product.DecreaseInventory(quantity);
        }
    }
}

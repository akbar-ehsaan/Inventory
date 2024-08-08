namespace inventory.domain.Core
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public int InventoryCount { get; private set; }
        public decimal Price { get; private set; }
        public decimal Discount { get; private set; } // Stored as percentage

        public Product()
        {
                
        }

        public Product(string title, int inventoryCount, decimal price, decimal discount)
        {
            if (string.IsNullOrWhiteSpace(title) || title.Length > 40)
                throw new ArgumentException("Product title must be between 1 and 40 characters.");

            Id = Guid.NewGuid();
            Title = title;
            InventoryCount = inventoryCount >= 0 ? inventoryCount : throw new ArgumentException("Inventory count cannot be negative.");
            Price = price > 0 ? price : throw new ArgumentException("Price must be greater than zero.");
            Discount = discount >= 0 && discount <= 100 ? discount : throw new ArgumentException("Discount must be between 0 and 100.");
        }

        public void IncreaseInventory(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");
            InventoryCount += amount;
        }

        public void DecreaseInventory(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");
            if (InventoryCount - amount < 0)
                throw new InvalidOperationException("Not enough inventory to decrease.");
            InventoryCount -= amount;
        }

        public decimal GetPriceWithDiscount()
        {
            return Price * (1 - Discount / 100);
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new ArgumentException("New price must be greater than zero.");
            Price = newPrice;
        }

        public void UpdateDiscount(decimal newDiscount)
        {
            if (newDiscount < 0 || newDiscount > 100)
                throw new ArgumentException("Discount must be between 0 and 100.");
            Discount = newDiscount;
        }
    }
}

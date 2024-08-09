using inventory.domain.Core;
using System.Runtime.CompilerServices;

namespace inventory.api.Dtoes
{
   
    public record AddProductDto(Guid Id, string Title, int InventoryCount, decimal Price, decimal Discount);

    public static partial class ProductExtensions
    {
        public static Product ToProductEntity(this AddProductDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            return new Product(
                dto.Title,
                dto.InventoryCount,
                dto.Price,
                dto.Discount
            );
        }
    }
}

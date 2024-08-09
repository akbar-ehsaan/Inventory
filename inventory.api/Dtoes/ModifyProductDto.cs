using inventory.domain.Core;

namespace inventory.api.Dtoes;

public record ModifyProductDto(string Id,string Title, decimal Price, decimal Discount);


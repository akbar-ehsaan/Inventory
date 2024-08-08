

using inventory.api.Application.Commands;
using inventory.api.Application.Queries;
using inventory.api.Dtoes;
using inventory.domain.Core;
using Microsoft.AspNetCore.Mvc;

namespace inventory.api.Controllers;


public class ProductController : BaseController
{
    [HttpPost]
    public Task<bool> AddProduct(AddProductDto command)
        => Mediator.Send(new AddProductCommand(command.ToProductEntity()));

    [HttpGet]
    public Task<Product> GetById(string productId)
        => Mediator.Send(new GetProductByIdQuery(productId));

    [HttpPut("IncreaseInventory")]
    public Task<bool> IncreaseInventory(IncreaseInventoryCommand cmd)
        => Mediator.Send(cmd);
}

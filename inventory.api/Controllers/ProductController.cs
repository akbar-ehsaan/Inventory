

using inventory.api.Application.Commands;
using inventory.api.Application.Queries;
using inventory.domain.Core;
using Microsoft.AspNetCore.Mvc;

namespace inventory.api.Controllers;


public class ProductController : BaseController
{
    [HttpPost]
    public Task<bool> AddProduct(AddProductCommand command)
        => Mediator.Send(command);

    [HttpGet]
    public Task<Product> GetById(string productId)
        => Mediator.Send(new GetProductByIdQuery(productId));

    [HttpPut]
    public Task<bool> IncreaseInventory(IncreaseInventoryCommand cmd)
        => Mediator.Send(cmd);
}

using inventory.api.Application.Commands;
using inventory.api.Dtoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace inventory.api.Controllers;

public class OrderController : BaseController
{
    [HttpPost("Buy")]
    public Task AddProduct(AddOrderDto command)
    => Mediator.Send(new AddOrderCommand(command));
}

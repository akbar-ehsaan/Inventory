

using inventory.api.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace inventory.api.Controllers;


public class ProductController : BaseController
{
    [HttpPost]
    public Task<bool> AddProduct(AddProductCommand command) 
        => Mediator.Send(command);
}

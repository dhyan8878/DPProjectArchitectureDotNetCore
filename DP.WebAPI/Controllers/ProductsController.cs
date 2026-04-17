using DP.Application.Features.Products.Commands.CreateProduct;
using DP.Application.Features.Products.Commands.DeleteProduct;
using DP.Application.Features.Products.Commands.UpdateProduct;
using DP.Application.Features.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DP.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetProductsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
    {
        if (id != command.Id) return BadRequest();

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProductCommand { Id = id });
        return NoContent();
    }
}
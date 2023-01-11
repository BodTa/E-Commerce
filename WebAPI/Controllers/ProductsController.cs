using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.DTOs;
using Application.Features.Products.Models;
using Application.Features.Products.Queries.GetByIdProduct;
using Application.Features.Products.Queries.GetListByCompanyIdProduct;
using Application.Features.Products.Queries.GetListProduct;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery]PageRequest pageRequest)
    {
        GetListProductQuery getListProductQuery = new() { PageRequest = pageRequest };
        ProductListModel result =await Mediator.Send(getListProductQuery);
        return Ok(result);
    }
    [HttpGet("withcompanyid")]
    public async Task<IActionResult> GetListByCompanyId([FromQuery] GetListByCompanyIdProduct getListByCompanyIdProduct)
    {
        ProductListModel result = await Mediator.Send(getListByCompanyIdProduct);
        return Ok(result);
    }

    [HttpGet("Id")]
    public async Task<IActionResult> GetProductById([FromQuery]GetByIdProductQuery getByIdProductQuery)
    {
        ProductDto result = await Mediator.Send(getByIdProductQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody]CreateProductCommand createProductCommand)
    {
        CreatedProductDto result = await Mediator.Send(createProductCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody]UpdateProductCommand updateProductCommand)
    {
        UpdatedProductDto result = await Mediator.Send(updateProductCommand);
        return Ok(result);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody]DeleteProductCommand deleteProductCommand)
    {
        DeletedProductDto result = await Mediator.Send(deleteProductCommand);
        return Ok(result);
    }
}

using Application.Features.Companies.Commands.CreateCompany;
using Application.Features.Companies.Commands.DeleteCompany;
using Application.Features.Companies.Commands.UpdateCompany;
using Application.Features.Companies.DTOs;
using Application.Features.Companies.Models;
using Application.Features.Companies.Queries.GetByIdCompany;
using Application.Features.Companies.Queries.GetByProductIdCompany;
using Application.Features.Companies.Queries.GetByStateCompany;
using Application.Features.Companies.Queries.GetListByCityCompany;
using Application.Features.Companies.Queries.GetListCompany;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompaniesController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery]PageRequest pageRequest)
    {
        GetListCompanyQuery getListCompanyQuery = new() { PageRequest = pageRequest };
        CompanyListModel result = await Mediator.Send(getListCompanyQuery);
        return Ok(getListCompanyQuery);
    }

    [HttpGet("getlistbycity")]
    public async Task<IActionResult> GetListByCity([FromQuery]GetListByCityCompanyQuery getListByCityCompanyQuery)
    {
        CompanyListModel result = await Mediator.Send(getListByCityCompanyQuery);
        return Ok(result);
    }

    [HttpGet("getlistbystate")]
    public async Task<IActionResult> GetListByState([FromQuery]GetListByStateCompanyQuery getListByStateCompanyQuery)
    {
        CompanyListModel result = await Mediator.Send(getListByStateCompanyQuery);
        return Ok(result);
    }

    [HttpGet("Id")]
    public async Task<IActionResult> GetById([FromRoute]GetByIdCompanyQuery getByIdCompanyQuery)
    {
        CompanyDto result = await Mediator.Send(getByIdCompanyQuery);
        return Ok(result);
    }

    [HttpGet("getbyproductid")]
    public async Task<IActionResult> GetByProductId([FromRoute]GetByProductIdCompanyQuery getByProductIdCompanyQuery)
    {
        CompanyDto result = await Mediator.Send(getByProductIdCompanyQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody]CreateCompanyCommand createCompanyCommand)
    {
        CreatedCompanyDto result = await Mediator.Send(createCompanyCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody]UpdateCompanyCommand updateCompanyCommand)
    {
        UpdatedCompanyDto result = await Mediator.Send(updateCompanyCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCompanyCommand deleteCompanyCommand)
    {
        DeletedCompanyDto result = await Mediator.Send(deleteCompanyCommand);
        return Ok(result);
    }
}

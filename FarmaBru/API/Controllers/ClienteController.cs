using API.Application.Commands;
using BusinessLogicalLayer.Interfaces;
using ClienteAPI.Application.Queries;
using Common;
using MediatR;
using MetaData.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ClienteAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly IMediator _mediatr;
    private readonly IRepository<Cliente> _repository;

    public ClienteController(IMediator mediatr, IRepository<Cliente> repository)
    {
        _mediatr = mediatr;
        _repository = repository;
    }

    // GET: Cliente
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _repository.GetAll());
    }

    // GET: Cliente/1
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int? id)
    {
        var query = new GetByIdQuery { Id = id };

        IResponse response = await _mediatr.Send(query);

        if (response.Errors.Any())
        {
            return BadRequest(response.Errors);
        }

        return Ok(response);
    }

    // POST: Cliente/Create
    [HttpPost("Create")]
    //[ValidateAntiForgeryToken] **** O que é isso?
    public async Task<IActionResult> Create(CadastraCommand command)
    {
        IResponse response = await _mediatr.Send(command);

        if (response.Errors.Any())
        {
            return BadRequest(response.Errors);
        }

        return Ok(response);
    }

    // GET: Cliente/Edit
    [HttpPut("Edit")]
    public async Task<IActionResult> Edit(AlteraCommand command)
    {
        IResponse response = await _mediatr.Send(command);

        if (response.Errors.Any())
        {
            return BadRequest(response.Errors);
        }

        return Ok(response);
    }

    // POST: Cliente/Deactivate/1
    [HttpPost("Deactivate/{id:int:min(1)}")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> Deactivate(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }

        var response = await _mediatr.Send(id.Value);
        return Ok(response);
    }
}

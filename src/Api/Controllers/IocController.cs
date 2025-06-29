using Api.Controllers.Common;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class IocController(IIocService iocService) : SecurityJwtController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var items = await iocService.GetAllAsync();

        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var item =
            await iocService.GetByIdAsync(id)
            ?? throw new IocNotFoundException($"Ioc with id: {id} not found");

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(IocCreateDto ioc)
    {
        await iocService.AddAsync(ioc);

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, IocUpdateDto ioc)
    {
        //var item = await iocService.GetByIdAsync(id);
        //if (item == null)
        //    return NotFound("error.not_found");

        await iocService.UpdateAsync(id, ioc);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await iocService.DeleteAsync(id);

        return NoContent();
    }
}

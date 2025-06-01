using Application.Interfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class IocController(IIocService iocService) : SecurityJwtController
{
    [HttpGet]
    public async Task<IActionResult> GetAllIocsAsync()
    {
        var items = await iocService.GetAllAsync();

        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetIocByIdAsync(int id)
    {
        var item = await iocService.GetByIdAsync(id);

        if (item == null)
            return NotFound("error.not_found");

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> CreateIocAsync(IocCreateDto ioc)
    {
        await iocService.AddAsync(ioc);

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIocAsync(int id, IocUpdateDto ioc)
    {
        //var item = await iocService.GetByIdAsync(id);
        //if (item == null)
        //    return NotFound("error.not_found");

        await iocService.UpdateAsync(id, ioc);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIocAsync(int id)
    {
        await iocService.DeleteAsync(id);

        return NoContent();
    }
}

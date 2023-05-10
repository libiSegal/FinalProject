
using BL.DataImplementation.ServiceInterfaces;
using BL.DTO;
using Microsoft.AspNetCore.Mvc;
namespace UI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LaundryController : ControllerBase
{
    private readonly ILaundryService _laundryService;
    public LaundryController(ILaundryService laundryService)
    {
        _laundryService = laundryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLaundry(LaundryDTO laundryDTO)
    {
        return Ok(await _laundryService.CreateObject(laundryDTO));
    }
    [HttpGet]
    public async Task<IActionResult> GetLaundry(string id)
    {
        return Ok(await _laundryService.GetObject(id));
    }
}


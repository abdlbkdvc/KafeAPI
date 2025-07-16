using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemServices _services;

        public MenuItemsController(IMenuItemServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMenuItems()
        {
            var result = await _services.GetAllMenuItems();
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMenuItem(int id)
        {
            var result = await _services.GetByIdMenuItem(id);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddMenuItem(CreateMenuItemDto dto)
        {
            var result = await _services.AddMenuItem(dto);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok("Menu Item oluşturuldu.");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem(UpdateMenuItemDto dto)
        {
            var result = await _services.UpdateMenuItem(dto);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok("İşlem başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var result = await _services.DeleteMenuItem(id);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok("Menü item silindi");
        }

    }
}

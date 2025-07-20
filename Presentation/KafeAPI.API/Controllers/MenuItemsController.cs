using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : BaseController
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
            return CreateResponse(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMenuItem(int id)
        {
            var result = await _services.GetByIdMenuItem(id);
            return CreateResponse(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddMenuItem(CreateMenuItemDto dto)
        {
            var result = await _services.AddMenuItem(dto);
            return CreateResponse(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem(UpdateMenuItemDto dto)
        {
            var result = await _services.UpdateMenuItem(dto);
            return CreateResponse(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var result = await _services.DeleteMenuItem(id);
            return CreateResponse(result);
        }

    }
}

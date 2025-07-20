using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.TableDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : BaseController
    {
        private readonly ITableServices _tableServices;

        public TablesController(ITableServices tableServices)
        {
            _tableServices = tableServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTablesAsync()
        {
            var result = await _tableServices.GetAllTableAsync();
            return CreateResponse(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdTable(int id)
        {
            var result = await _tableServices.GetByIdTableAsync(id);
            return CreateResponse(result);
        }
        [HttpGet("getbytablenumber")]
        public async Task<IActionResult> GetByTableNumber(int tableNumber)
        {
            var result = await _tableServices.GetByTableNumberAsync(tableNumber);
            return CreateResponse(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddTablesAsync(CreateTableDto dto)
        {
            var result = await _tableServices.CreateTableAsync(dto);
            return CreateResponse(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTable(UpdateTableDto dto)
        {
            var result = await _tableServices.UpdateTableAsync(dto);
            return CreateResponse(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTableAsync(int id)
        {
            var result = await _tableServices.DeleteTableAsync(id);
            return CreateResponse(result);
        }
        [HttpGet("getallisactivetablesgeneric")]
        public async Task<IActionResult> GetAllIsActiveTablesGeneric()
        {
            var result = await _tableServices.GetAllActiveTableGeneric();
            return CreateResponse(result);
        }
        [HttpGet("getallisactivetables")]
        public async Task<IActionResult> GetAllIsActiveTables()
        {
            var result = await _tableServices.GetAllActiveTables();
            return CreateResponse(result);
        }
        [HttpPut("updatetablestatusbyid")]
        public async Task<IActionResult> UpdateTableStatusById(int id)
        {
            var result = await _tableServices.UpdateTableStatusById(id);
            return CreateResponse(result);
        }
        [HttpPut("updatetablestatusbytablenumber")]
        public async Task<IActionResult> UpdateTableStatusByTableNumber(int tableNumber)
        {
            var result = await _tableServices.UpdateTableStatusByTableNumber(tableNumber);
            return CreateResponse(result);
        }
    }
}

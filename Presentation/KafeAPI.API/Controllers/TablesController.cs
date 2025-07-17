using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.TableDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
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
        public async Task<IActionResult> GetByIdTable(int id)
        {
            var result = await _tableServices.GetByIdTableAsync(id);
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
        [HttpGet("getbytablenumber")]
        public async Task<IActionResult> GetByTableNumber(int tableNumber)
        {
            var result = await _tableServices.GetByTableNumberAsync(tableNumber);
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
        public async Task<IActionResult> AddTablesAsync(CreateTableDto dto)
        {
            var result = await _tableServices.CreateTableAsync(dto);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.DuplicateError)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTable(UpdateTableDto dto)
        {
            var result = await _tableServices.UpdateTableAsync(dto);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTableAsync(int id)
        {
            var result = await _tableServices.DeleteTableAsync(id);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("getallisactivetablesgeneric")]
        public async Task<IActionResult> GetAllIsActiveTablesGeneric()
        {
            var result = await _tableServices.GetAllActiveTableGeneric();
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
        [HttpGet("getallisactivetables")]
        public async Task<IActionResult> GetAllIsActiveTables()
        {
            var result = await _tableServices.GetAllActiveTables();
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
        [HttpPut("updatetablestatusbyid")]
        public async Task<IActionResult> UpdateTableStatusById(int id)
        {
            var result = await _tableServices.UpdateTableStatusById(id);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
            }
            return Ok(result);
        }
        [HttpPut("updatetablestatusbytablenumber")]
        public async Task<IActionResult> UpdateTableStatusByTableNumber(int tableNumber)
        {
            var result = await _tableServices.UpdateTableStatusByTableNumber(tableNumber);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound)
                {
                    return Ok(result);
                }
            }
            return Ok(result);
        }
    }
}

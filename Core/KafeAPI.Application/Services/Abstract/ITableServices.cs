using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.TableDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface ITableServices
    {
        Task<ResponseDto<object>> CreateTableAsync(CreateTableDto dto);
        Task<ResponseDto<object>> UpdateTableAsync(UpdateTableDto dto);
        Task<ResponseDto<object>> DeleteTableAsync(int id);
        Task<ResponseDto<List<ResultTableDto>>> GetAllTableAsync();
        Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTableGeneric();
        Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTables();
        Task<ResponseDto<DetailTableDto>> GetByIdTableAsync(int id);
        Task<ResponseDto<DetailTableDto>> GetByTableNumberAsync(int tableNumber);
        Task<ResponseDto<object>> UpdateTableStatusById(int id);
        Task<ResponseDto<object>> UpdateTableStatusByTableNumber(int tableNumber);
    }
}

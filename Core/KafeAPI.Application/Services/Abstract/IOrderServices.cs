using KafeAPI.Application.Dtos.OrderDtos;
using KafeAPI.Application.Dtos.ResponseDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IOrderServices
    {
        Task<ResponseDto<List<ResultOrderDto>>> GetAllOrder();
        Task<ResponseDto<DetailOrderDto>> GetOrderById(int id);
        Task<ResponseDto<object>> AddOrder(CreateOrderDto dto);
        Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto dto);
        Task<ResponseDto<object>> DeleteOrder(int id);
    }
}

using AutoMapper;
using KafeAPI.Application.Dtos.OrderDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class OrderServices : IOrderServices
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IMapper _mapper;

        public OrderServices(IGenericRepository<Order> orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public Task<ResponseDto<object>> AddOrder(CreateOrderDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<object>> DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto<List<ResultOrderDto>>> GetAllOrder()
        {
            try
            {
                var orderdb = await _orderRepository.GetAllAsync();
                if (orderdb.Count == 0)
                {
                    return new ResponseDto<List<ResultOrderDto>>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Sipariş bulunamadı."
                    };
                }
                var result = _mapper.Map<List<ResultOrderDto>>(orderdb);
                return new ResponseDto<List<ResultOrderDto>>
                {
                    Success = true,
                    Data = result,
                    Message = "Siparişler başarıyla getirildi."
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<List<ResultOrderDto>>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu."
                };
            }
        }

        public async Task<ResponseDto<DetailOrderDto>> GetOrderById(int id)
        {
            try
            {
                var orderdb = await _orderRepository.GetByIdAsync(id);
                if (orderdb == null)
                {
                    return new ResponseDto<DetailOrderDto>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Sipariş bulunamadı."
                    };
                }
                var result = _mapper.Map<DetailOrderDto>(orderdb);
                return new ResponseDto<DetailOrderDto>
                {
                    Success = true,
                    Data = result,
                    Message = "Sipariş başarıyla getirildi."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailOrderDto>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu."
                };

            }
        }

        public Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

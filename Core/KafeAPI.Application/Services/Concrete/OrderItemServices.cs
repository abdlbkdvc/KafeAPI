using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.OrderItemDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;
using System.Collections.Generic;

namespace KafeAPI.Application.Services.Concrete
{
    public class OrderItemServices : IOrderItemServices
    {
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderItemDto> _createOrderItemValidator;
        private readonly IValidator<UpdateOrderItemDto> _updateOrderItemValidator;

        public OrderItemServices(IGenericRepository<OrderItem> orderItemRepository, IMapper mapper, IValidator<CreateOrderItemDto> createOrderItemValidator, IValidator<UpdateOrderItemDto> updateOrderItemValidator)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
            _createOrderItemValidator = createOrderItemValidator;
            _updateOrderItemValidator = updateOrderItemValidator;
        }

        public async Task<ResponseDto<object>> AddOrderItem(CreateOrderItemDto dto)
        {
            try
            {
                var validate = await _createOrderItemValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.ValidationError,
                        Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage))
                    };
                }
                var result = _mapper.Map<OrderItem>(dto);
                await _orderItemRepository.CreateAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = result,
                    Message = "Menu itemları başarılı bir şekilde eklendi."
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu.",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> DeleteOrderItem(int id)
        {
            try
            {
                var checkOrderItemdb = await _orderItemRepository.GetByIdAsync(id);
                if (checkOrderItemdb == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş itemi bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _orderItemRepository.DeleteAsync(checkOrderItemdb);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Sipariş iteminiz başarılı bir şekilde silindi."
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu."
                };
            }
        }

        public async Task<ResponseDto<List<ResultOrderItemDto>>> GetAllOrderItems()
        {
            try
            {
                var orderItemdb = await _orderItemRepository.GetAllAsync();
                if (orderItemdb.Count == 0)
                {
                    return new ResponseDto<List<ResultOrderItemDto>>()
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Menu item bulunamadı."
                    };
                }
                var result = _mapper.Map<List<ResultOrderItemDto>>(orderItemdb);
                return new ResponseDto<List<ResultOrderItemDto>>
                {
                    Success = true,
                    Data = result,
                    Message = "Başarılı bir şekilde menu itemlar geldi."
                };

            }
            catch (Exception ex)
            {

                return new ResponseDto<List<ResultOrderItemDto>>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu."
                };
            }
        }

        public async Task<ResponseDto<DetailOrderItemDto>> GetOrderItemById(int id)
        {
            try
            {
                var db = await _orderItemRepository.GetByIdAsync(id);
                if (db == null)
                {
                    return new ResponseDto<DetailOrderItemDto>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Menu item bulunamadı."
                    };
                }
                var result = _mapper.Map<DetailOrderItemDto>(db);
                return new ResponseDto<DetailOrderItemDto>
                {
                    Success = true,
                    Data = result,
                    Message = "Menu item başarıyla getirildi."
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<DetailOrderItemDto>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu."
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateOrderItem(UpdateOrderItemDto dto)
        {
            try
            {
                var validate = await _updateOrderItemValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.ValidationError,
                        Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage))
                    };
                }
                var orderItemdb = await _orderItemRepository.GetByIdAsync(dto.Id);
                if (orderItemdb == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Böyle bir Sipariş itemi bulunamadı."
                    };
                }
                var result = _mapper.Map(dto, orderItemdb);
                await _orderItemRepository.UpdateAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Sipariş itemları başarılı bir şekilde güncellendi."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu.",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}

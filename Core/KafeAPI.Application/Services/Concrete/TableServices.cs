using AutoMapper;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.TableDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class TableServices : ITableServices
    {
        private readonly IGenericRepository<Table> _repository;
        private readonly IMapper _mapper;

        public TableServices(IGenericRepository<Table> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<ResponseDto<object>> CreateTableAsync(CreateTableDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<object>> DeleteTableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto<List<ResultTableDto>>> GetAllTableAsync()
        {
            try
            {
                var rp = await _repository.GetAllAsync();
                if (rp.Count == 0)
                {
                    return new ResponseDto<List<ResultTableDto>>
                    {
                        Success = false,
                        Data = null,
                        ErrorCodes = ErrorCodes.NotFound,
                        Message = "Masalar bulunamadı."
                    };
                }
                var result = _mapper.Map<List<ResultTableDto>>(rp);
                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = true,
                    Data = result,
                    Message = "Masalar başarıyla getirildi."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = false,
                    Data = null,
                    ErrorCodes = ErrorCodes.Exception,
                    Message = "Bir hata oluştu."
                };

            }
        }

        public async Task<ResponseDto<DetailTableDto>> GetByIdTableAsync(int id)
        {
            try
            {
                var rp = await _repository.GetByIdAsync(id);
                if (rp == null)
                {
                    return new ResponseDto<DetailTableDto>
                    {
                        Success = false,
                        Data = null,
                        ErrorCodes = ErrorCodes.NotFound,
                        Message = "Masa bulunamadı."
                    };
                }
                var result = _mapper.Map<DetailTableDto>(rp);
                return new ResponseDto<DetailTableDto>
                {
                    Success = true,
                    Data = result,
                    Message = "Masa başarıyla getirildi."
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailTableDto>
                {
                    Success = false,
                    Data = null,
                    ErrorCodes = ErrorCodes.Exception,
                    Message = "Bir hata oluştu."
                };
            }
        }

        public Task<ResponseDto<DetailTableDto>> GetByTableNumberAsync(int tableNumber)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<object>> UpdateTableAsync(UpdateTableDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

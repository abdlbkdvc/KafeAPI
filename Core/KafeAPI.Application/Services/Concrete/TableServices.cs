using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<CreateTableDto> _createTableValidator;
        private readonly IValidator<UpdateTableDto> _updateTableValidator;
        private readonly ITableRepository _tableRepository;
        public TableServices(IGenericRepository<Table> repository, IMapper mapper, IValidator<CreateTableDto> createTableValidator, IValidator<UpdateTableDto> updateTableValidator, ITableRepository tableRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _createTableValidator = createTableValidator;
            _updateTableValidator = updateTableValidator;
            _tableRepository = tableRepository;
        }

        public async Task<ResponseDto<object>> CreateTableAsync(CreateTableDto dto)
        {
            try
            {
                var validate = await _createTableValidator.ValidateAsync(dto);
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
                var checkTable = await _repository.GetByIdAsync(dto.TableNumber);
                if (checkTable != null)
                {
                    return new ResponseDto<object>
                    {
                        Success = true,
                        Data = null,
                        Message = "Eklemek istediğiniz masa numarası mevcuttur.",
                        ErrorCode = ErrorCodes.DuplicateError
                    };
                }
                var result = _mapper.Map<Table>(dto);
                await _repository.CreateAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = result,
                    Message = "Masa başarılı bir şekilde eklendi."
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

        public async Task<ResponseDto<object>> DeleteTableAsync(int id)
        {
            try
            {
                var rp = await _repository.GetByIdAsync(id);
                if (rp == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Masa bulunamadı."
                    };
                }
                await _repository.DeleteAsync(rp);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Masa başarılı bir şekilde silindi."
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

        public async Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTableGeneric()
        {
            try
            {
                var rp = await _repository.GetAllAsync();
                rp = rp.Where(x => x.IsActive is true).ToList();

                if (rp.Count == 0)
                {
                    return new ResponseDto<List<ResultTableDto>>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Masalar bulunamadı."
                    };
                }
                var result = _mapper.Map<List<ResultTableDto>>(rp);
                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = true,
                    Data = result,
                    Message = "Aktif masalar başarılı bir şekilde getirildi."
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu."
                };
            }
        }

        public async Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTables()
        {
            try
            {
                var rp = await _tableRepository.GetAllActiveTablesAsync();

                if (rp.Count == 0)
                {
                    return new ResponseDto<List<ResultTableDto>>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Masalar bulunamadı."
                    };
                }
                var result = _mapper.Map<List<ResultTableDto>>(rp);
                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = true,
                    Data = result,
                    Message = "Masalar başarılı bir şekilde getirildi."
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<List<ResultTableDto>>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu."
                };
            }
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
                        ErrorCode = ErrorCodes.NotFound,
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
                    ErrorCode = ErrorCodes.Exception,
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
                        ErrorCode = ErrorCodes.NotFound,
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
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu."
                };
            }
        }

        public async Task<ResponseDto<DetailTableDto>> GetByTableNumberAsync(int tableNumber)
        {
            try
            {
                var table = await _tableRepository.GetByTableNumberAsync(tableNumber);
                if (table == null)
                {
                    return new ResponseDto<DetailTableDto>
                    {
                        Success = false,
                        Data = null,
                        Message = "Masa Bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<DetailTableDto>(table);
                return new ResponseDto<DetailTableDto>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<DetailTableDto>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu."
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateTableAsync(UpdateTableDto dto)
        {
            try
            {
                var validate = await _updateTableValidator.ValidateAsync(dto);
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
                var rp = await _repository.GetByIdAsync(dto.Id);
                if (rp == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Masa bulunamadı."
                    };
                }
                //var checkTable = await _repository.GetByIdAsync(dto.TableNumber);
                var result = _mapper.Map(dto, rp);
                await _repository.UpdateAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Masa başarılı bir şekilde güncellendi."
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

        public async Task<ResponseDto<object>> UpdateTableStatusById(int id)
        {
            try
            {
                var rp = await _repository.GetByIdAsync(id);
                if (rp == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Masa bulunamadı."
                    };
                }
                //var checkTable = await _repository.GetByIdAsync(dto.TableNumber);
                rp.IsActive = !rp.IsActive;
                await _repository.UpdateAsync(rp);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Masa başarılı bir şekilde güncellendi."
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

        public async Task<ResponseDto<object>> UpdateTableStatusByTableNumber(int tableNumber)
        {
            try
            {
                var rp = await _tableRepository.GetByTableNumberAsync(tableNumber);
                if (rp == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Masa bulunamadı."
                    };
                }
                //var checkTable = await _repository.GetByIdAsync(dto.TableNumber);
                rp.IsActive = !rp.IsActive;
                await _repository.UpdateAsync(rp);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Masa başarılı bir şekilde güncellendi."
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
    }
}

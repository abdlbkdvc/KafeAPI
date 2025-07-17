using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _createCategoryValidator;
        private readonly IValidator<UpdateCategoryDto> _updateCategoryValidator;

        public CategoryServices(IGenericRepository<Category> repository, IMapper mapper, IValidator<CreateCategoryDto> createCategoryValidator, IValidator<UpdateCategoryDto> updateCategoryValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryValidator = updateCategoryValidator;
        }

        public async Task<ResponseDto<object>> AddCategory(CreateCategoryDto dto)
        {
            try
            {
                var validate = await _createCategoryValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var category = _mapper.Map<Category>(dto);
                await _repository.CreateAsync(category);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Kategori oluşturuldu."
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

        public async Task<ResponseDto<object>> DeleteCategory(int id)
        {
            try
            {
                var category = await _repository.GetByIdAsync(id);
                if (category == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Kategori bulunmadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _repository.DeleteAsync(category);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Kategori Silindi."
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

        public async Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategories()
        {
            try
            {
                var categories = await _repository.GetAllAsync();
                if (categories.Count == 0)
                {
                    return new ResponseDto<List<ResultCategoryDto>>
                    {
                        Success = false,
                        Message = "Kategori bulunumadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultCategoryDto>>(categories);

                return new ResponseDto<List<ResultCategoryDto>>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultCategoryDto>>
                {
                    Success = false,
                    Message = "Bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }

        }

        public async Task<ResponseDto<DetailCategoryDto>> GetByIdCategory(int id)
        {
            try
            {
                var category = await _repository.GetByIdAsync(id);
                if (category == null)
                {
                    return new ResponseDto<DetailCategoryDto>
                    {
                        Success = false,
                        Message = "Kategori bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<DetailCategoryDto>(category);
                return new ResponseDto<DetailCategoryDto>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<DetailCategoryDto>
                {
                    Success = false,
                    Message = "Bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }


        }

        public async Task<ResponseDto<object>> UpdateCategory(UpdateCategoryDto dto)
        {
            try
            {
                var validate = await _updateCategoryValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join(",", validate.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var categorydb = await _repository.GetByIdAsync(dto.Id);
                if (categorydb == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Kategori bulunumadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var category = _mapper.Map(dto, categorydb);

                await _repository.UpdateAsync(category);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Kategori güncellendi."
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Data = null,
                    Message = "Bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }

        }
    }
}

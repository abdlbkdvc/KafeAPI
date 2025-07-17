using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class MenuItemServices : IMenuItemServices
    {
        private readonly IGenericRepository<MenuItem> _repository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateMenuItemDto> _createMenuItemValidator;
        private readonly IValidator<UpdateMenuItemDto> _updateMenuItemValidator;
        public MenuItemServices(IGenericRepository<MenuItem> repository, IMapper mapper, IValidator<CreateMenuItemDto> createMenuItemValidator, IValidator<UpdateMenuItemDto> updateMenuItemValidator, IGenericRepository<Category> categoryRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _createMenuItemValidator = createMenuItemValidator;
            _updateMenuItemValidator = updateMenuItemValidator;
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseDto<object>> AddMenuItem(CreateMenuItemDto dto)
        {
            try
            {
                var validate = await _createMenuItemValidator.ValidateAsync(dto);
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
                var checkCategory = await _categoryRepository.GetByIdAsync(dto.CategoryId);
                if (checkCategory == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = dto,
                        Message = "Eklemek istediğiniz kategori bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var menuItem = _mapper.Map<MenuItem>(dto);
                await _repository.CreateAsync(menuItem);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = menuItem,
                    Message = "Menu Item başarıyla eklendi."
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<object>
                {
                    Success = false,
                    Message = "Bir hata oluştu.",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> DeleteMenuItem(int id)
        {
            try
            {
                var menuItem = await _repository.GetByIdAsync(id);
                if (menuItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Message = "Menu item bulunamadı",
                        ErrorCode = ErrorCodes.NotFound,
                        Data = null
                    };
                }
                await _repository.DeleteAsync(menuItem);
                return new ResponseDto<object>
                {
                    Success = true,
                    Message = "Menu Item başarılı bir şekilde silindi.",
                    Data = menuItem
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

        public async Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems()
        {
            try
            {

                var menuItems = await _repository.GetAllAsync();
                var category = await _categoryRepository.GetAllAsync();
                if (menuItems.Count == 0)
                {
                    return new ResponseDto<List<ResultMenuItemDto>>
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Menu items bulunamadı.",
                        Data = null
                    };
                }
                var result = _mapper.Map<List<ResultMenuItemDto>>(menuItems);
                return new ResponseDto<List<ResultMenuItemDto>>
                {
                    Success = true,
                    Data = result,
                    Message = "Menu Items başarılı bir şekilde getirildi."
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<List<ResultMenuItemDto>>
                {
                    Success = false,
                    Message = "Bir hata oluştu.",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailMenuItemDto>> GetByIdMenuItem(int id)
        {
            try
            {
                var menuItem = await _repository.GetByIdAsync(id);
                var category = await _categoryRepository.GetByIdAsync(menuItem.CategoryId);
                if (menuItem == null)
                {
                    return new ResponseDto<DetailMenuItemDto>
                    {
                        Success = false,
                        ErrorCode= ErrorCodes.NotFound,
                        Message = "Bir menu item bulunamadı.",
                        Data = null
                    };
                }
                var result = _mapper.Map<DetailMenuItemDto>(menuItem);
                return new ResponseDto<DetailMenuItemDto>
                {
                    Success = true,
                    Data = result,
                    Message = "Menu item başarılı bir şekilde getirildi."
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<DetailMenuItemDto>
                {
                    Success = false,
                    Message = "Bir hata oluştu.",
                    ErrorCode = ErrorCodes.Exception
                };
            }

        }

        public async Task<ResponseDto<object>> UpdateMenuItem(UpdateMenuItemDto dto)
        {
            try
            {
                var validate = await _updateMenuItemValidator.ValidateAsync(dto);
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
                var menuItem = await _repository.GetByIdAsync(dto.Id);
                if (menuItem == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Menu item bulunamadı."
                    };
                }
                var checkCategory = await _categoryRepository.GetByIdAsync(dto.CategoryId);
                if (checkCategory == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = dto,
                        Message = "Eklemek istediğiniz kategori bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var newmenuItem = _mapper.Map(dto, menuItem);
                await _repository.UpdateAsync(newmenuItem);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = newmenuItem,
                    Message = "Menu Item başarıyla güncellendi."
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

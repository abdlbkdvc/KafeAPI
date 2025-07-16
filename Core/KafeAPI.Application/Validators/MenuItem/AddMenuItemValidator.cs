using FluentValidation;
using KafeAPI.Application.Dtos.MenuItemDtos;

namespace KafeAPI.Application.Validators.MenuItem
{
    public class AddMenuItemValidator : AbstractValidator<CreateMenuItemDto>
    {
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public decimal Price { get; set; }
        //public string ImageUrl { get; set; }
        //public bool IsAvailable { get; set; }
        //public int CategoryId { get; set; }
        public AddMenuItemValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Menu Item adı boş olamaz.")
                .Length(2, 40).WithMessage("Menu Item adı 2 ile 40 karakter arasında olmak zorundadır.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Menu Item açıklaması boş olamaz.")
                .Length(5, 100).WithMessage("Menu Item açıklaması 5 ile 100 karakter arasında olmak zorundadır.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Menu Item fiyatı boş geçilemez.")
                .GreaterThan(0).WithMessage("Menu Item fiyatı 0'dan büyük olmak zorundadır.");
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Menu Item fotoğraf Url'i boş olamaz.");
        }
    }
}

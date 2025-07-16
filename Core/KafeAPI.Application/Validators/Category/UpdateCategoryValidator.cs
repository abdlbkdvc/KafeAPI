using FluentValidation;
using KafeAPI.Application.Dtos.CategoryDtos;

namespace KafeAPI.Application.Validators.Category
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori Adı boş olamaz.")
                .Length(3, 30).WithMessage("Kategori adının uzunluğu 3 ile 30 karakter arasında olması gerekmektedir.");
        }
    }
}

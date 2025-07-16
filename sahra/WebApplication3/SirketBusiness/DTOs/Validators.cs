using FluentValidation;

namespace SirketBusiness.DTOs
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adı boş olamaz");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalı");
        }
    }

    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün adı boş olamaz");
        }
    }

    public class ProjectCreateDtoValidator : AbstractValidator<ProjectCreateDto>
    {
        public ProjectCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Proje adı boş olamaz");
        }
    }

    public class DepartmentCreateDtoValidator : AbstractValidator<DepartmentCreateDto>
    {
        public DepartmentCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Departman adı boş olamaz");
        }
    }

    public class SensorCreateDtoValidator : AbstractValidator<SensorCreateDto>
    {
        public SensorCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Sensör adı boş olamaz");
            RuleFor(x => x.SensorType).NotEmpty().WithMessage("Sensör tipi boş olamaz");
        }
    }

    public class SensorDataCreateDtoValidator : AbstractValidator<SensorDataCreateDto>
    {
        public SensorDataCreateDtoValidator()
        {
            RuleFor(x => x.SensorId).GreaterThan(0).WithMessage("SensorId pozitif olmalı");
            RuleFor(x => x.Value).NotNull().WithMessage("Değer boş olamaz");
            RuleFor(x => x.Timestamp).NotEmpty().WithMessage("Zaman bilgisi boş olamaz");
        }
    }
} 
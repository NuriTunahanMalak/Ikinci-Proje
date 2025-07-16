using FluentValidation;
using YeniProjeDeneme1.Dtos;
namespace YeniProjeDeneme1.Validator
{
    public class MainValidator
    {
    }
    //SENSOR_DATA
    public class Sensor_DataCreateValidator:AbstractValidator<SensorDataCreateDto>
    {
        public Sensor_DataCreateValidator()
        {
            RuleFor(x => x.SensorId).GreaterThan(0).WithMessage("Sensor_ID must be greater than 0.");
            RuleFor(x => x.data).GreaterThan(0).WithMessage("Sensor data must be greater than 0.");

        }
    }
    public class Sensor_DataUpdateValidator : AbstractValidator<SensorDataUpdateDto>
    {
        public Sensor_DataUpdateValidator()
        {
            RuleFor(x => x.SensorId).GreaterThan(0).WithMessage("Sensor ID must be greater than 0.");
            RuleFor(x => x.data).GreaterThan(0).WithMessage("Sensor data must be greater than 0.");
        }
    }
    //SENSOR
    public class SensorCreateValidator : AbstractValidator<SensorCreateDto>
    {
        public SensorCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Sensor name cannot be empty.");
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Product ID must be greater than 0.");
        }
    }
    public class SensorUpdateValidator : AbstractValidator<SensorUpdateDto>
    {
        public SensorUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Sensor name cannot be empty.");
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Product ID must be greater than 0.");
        }
    }
    //PRODUCT
    public class ProductCreateValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateValidator()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name cannot be empty.");
        }
    }
    public class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name cannot be empty.");
        }
    }
    //PROJECTPRODUCT
    public class ProjectProductCreateValidator : AbstractValidator<ProjectProductCreateDto>
    {
        public ProjectProductCreateValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Product ID must be greater than 0.");
            RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("Project ID must be greater than 0.");
        }
    }
    public class ProjectProductUpdateValidator : AbstractValidator<ProjectProductUpdateDto>
    {
        public ProjectProductUpdateValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Project Product ID must be greater than 0.");
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Product ID must be greater than 0.");
            RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("Project ID must be greater than 0.");
        }
    }
    //PROJECT
    public class ProjectCreateValidator : AbstractValidator<ProjectCreateDto>
    {
        public ProjectCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Project name cannot be empty.");
        }
    }
    public class ProjectUpdateValidator : AbstractValidator<ProjectUpdateDto>
    {
        public ProjectUpdateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Project name cannot be empty.");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Project description cannot be empty.");
        }
    }
    //PROJECTUSER
    public class ProjectUserCreateValidator : AbstractValidator<ProjectUserCreateDto>
    {
        public ProjectUserCreateValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("User ID must be greater than 0.");
            RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("Project ID must be greater than 0.");
        }
    }
    public class ProjectUserUpdateValidator : AbstractValidator<ProjectUserUpdateDto>
    {
        public ProjectUserUpdateValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Project User ID must be greater than 0.");
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("User ID must be greater than 0.");
            RuleFor(x => x.ProjectId).GreaterThan(0).WithMessage("Project ID must be greater than 0.");
        }
    }
    //USER
    public class UserCreateValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateValidator()
        {
            
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name cannot be empty.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("User surname cannot be empty.");
        }
    }
    public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name cannot be empty.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("User surname cannot be empty.");
        }
    }





}

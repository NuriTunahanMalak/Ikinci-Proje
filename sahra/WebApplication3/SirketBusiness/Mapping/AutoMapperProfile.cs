using AutoMapper;
using SirketEntites;

using SirketBusiness.DTOs;

namespace SirketBusiness.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();

            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.UserIds, opt => opt.MapFrom(src => src.UserProjects.Select(up => up.UserId)))
                .ForMember(dest => dest.ProductIds, opt => opt.MapFrom(src => src.ProjectProducts.Select(pp => pp.ProductId)));

            CreateMap<ProjectCreateDto, Project>()
                .ForMember(dest => dest.UserProjects, opt => opt.MapFrom(src => src.UserIds != null ? src.UserIds.Select(uid => new UserProject { UserId = uid }) : new List<UserProject>()))
                .ForMember(dest => dest.ProjectProducts, opt => opt.MapFrom(src => src.ProductIds != null ? src.ProductIds.Select(pid => new ProjectProduct { ProductId = pid }) : new List<ProjectProduct>()));

            CreateMap<ProjectUpdateDto, Project>()
                .ForMember(dest => dest.UserProjects, opt => opt.MapFrom(src => src.UserIds != null ? src.UserIds.Select(uid => new UserProject { UserId = uid }) : new List<UserProject>()))
                .ForMember(dest => dest.ProjectProducts, opt => opt.MapFrom(src => src.ProductIds != null ? src.ProductIds.Select(pid => new ProjectProduct { ProductId = pid }) : new List<ProjectProduct>()));

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();


            CreateMap<Sensor, SensorDto>();
            CreateMap<SensorCreateDto, Sensor>();
           


            CreateMap<SensorData, SensorDataDto>().ReverseMap();
            CreateMap<SensorDataCreateDto, SensorData>();

            CreateMap<Department, DepartmentDto>().ReverseMap(); 
            CreateMap<DepartmentCreateDto, Department>();
        }
    }
}

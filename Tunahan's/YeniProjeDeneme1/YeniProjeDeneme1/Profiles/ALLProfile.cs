using AutoMapper;
using YeniProjeDeneme1.Entities;
using YeniProjeDeneme1.Dtos;

namespace YeniProjeDeneme1.Profiles
{
    public class ALLProfile:Profile
    {
        public ALLProfile()
        {
            CreateMap<User, UserReadDto>()
                .ForMember(desk=>desk.ProjectUsers,opt=> opt.MapFrom(src=> src.ProjectUsers.Select(
                    pu => new ProjectUserDtom { 
                        ProjectId = pu.ProjectId 
                    })));
            CreateMap<User, UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<UserCreateDto, User>();

            CreateMap<ProjectUser, ProjectUserReadDto>();
            CreateMap<ProjectUserCreateDto, ProjectUser>();
            CreateMap<ProjectUserUpdateDto, ProjectUser>();
            //-----------------------------------------------------------------------------------------------
            CreateMap<Project, ProjectReadDto>()
                .ForMember(dest => dest.ProjectProducts, opt => opt.MapFrom(src => src.ProjectProducts.Select(
                    pu => new ProjectProductDtoa
                    {
                        ProductId=pu.ProjectId,
                    })))
                .ForMember(dest => dest.ProjectUsers, opt => opt.MapFrom(src => src.ProjectUsers.Select(
                    pu => new ProjectUserDtoa
                    {
                        UserId = pu.UserId
                    })));
            CreateMap<ProjectCreateDto, Project>();
            CreateMap<ProjectUpdateDto, Project>();
            //-----------------------------------------------------------------------------------------------
            CreateMap<ProjectProduct, ProjectProductReadDto>();
            CreateMap<ProjectProductCreateDto, ProjectProduct>();
            CreateMap<ProjectProductUpdateDto, ProjectProduct>();
            //-----------------------------------------------------------------------------------------------
            CreateMap<Product, ProductDtoRead>()
                .ForMember(dest=> dest.Sensors, opt => opt.MapFrom(src => src.ProjectProducts.Select(
                    pp => new ProjectProductDtoa
                    {
                        ProductId = pp.ProductId
                    })))
                .ForMember(dest => dest.Sensors, opt => opt.MapFrom(src => src.Sensor.Select(
                    pp => new SensorDto_p
                    {
                        Id = pp.Id,
                        Name = pp.Name

                    })));
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
            //-----------------------------------------------------------------------------------------------
            
            CreateMap<Sensor, SensorReadDto>()
                .ForMember(dest => dest.SensorDatas, opt => opt.MapFrom(src =>
                    src.Sensor_Datas.Select(sd => new SensorDataDto
                    {
                        SensorId = sd.SensorId,
                        Data = sd.data
                    }).ToList()))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));
            CreateMap<SensorCreateDto, Sensor>();
            CreateMap<SensorUpdateDto, Sensor>();
            //-----------------------------------------------------------------------------------------------
            CreateMap<SensorDataDto , SensorDataReadDto>();
            CreateMap<SensorDataCreateDto, SensorDataDto>();
            CreateMap<SensorDataUpdateDto, SensorDataDto>();







        }
    }
}

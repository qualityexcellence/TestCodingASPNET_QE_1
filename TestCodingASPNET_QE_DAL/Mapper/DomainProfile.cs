using AutoMapper;

namespace TestCodingASPNET_QE_DAL
{
    public class DomainProfile : Profile
    {
        public MapperConfiguration Map()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterVM, UserMaster>();
                cfg.CreateMap<UserInfoVM, UserMaster>();
                cfg.CreateMap<CarVM, CarMaster>();
                cfg.CreateMap<CarInfoVM, CarMaster>();
            });
            return config;
        }
    }
}
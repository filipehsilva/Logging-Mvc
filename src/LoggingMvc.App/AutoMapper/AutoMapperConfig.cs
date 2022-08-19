using AutoMapper;
using LoggingMvc.App.ViewModels;
using LoggingMvc.Business.Models;

namespace LoggingMvc.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Log, LogViewModel>().ReverseMap();
        }
    }
}

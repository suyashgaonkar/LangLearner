using AutoMapper;
using LangLearner.Models.Dtos.Requests;
using LangLearner.Models.Dtos.Responses;
using LangLearner.Models.Entities;

namespace LangLearner
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<LoginUserDto, User>();
            CreateMap<User, UserStatsDto>();
            CreateMap<Language, LanguageDto>();
            CreateMap<LanguageDto, Language>();
        }
    }
}

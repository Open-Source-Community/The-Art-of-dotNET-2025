using static System.Runtime.InteropServices.JavaScript.JSType;
using Task_Manager.ApiService.DTOs;
using AutoMapper;
using Task_Manager.Web.Data;

namespace Task_Manager.ApiService
{
    public class DTOsMappingProfile : Profile
    {
        public DTOsMappingProfile()
        {
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => false)) // Explicitly set to false
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0));

            CreateMap<UpdateUserDto, User>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // For updates, ignoring nulls

            CreateMap<CreateTodoTaskDto, TodoTask>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0));
            CreateMap<UpdateTodoTaskDto, TodoTask>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
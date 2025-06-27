using AutoMapper;
using Qwin.WebApiTemp.User.DTOs;
using Qwin.WebApiTemp.User.Entity;
namespace Qwin.WebApiTemp.AutoMapperProfile
{
    public class QwinProfile:Profile
    {
        public QwinProfile()
        {
            //Properties are same
            //CreateMap<UserDto, UserEntity>();
            //If different
            CreateMap<UserDto, UserEntity>()
                .ForMember(p=>p.Email,q=>q.MapFrom(p=>p.EmailId)) ;

        }
    }
}

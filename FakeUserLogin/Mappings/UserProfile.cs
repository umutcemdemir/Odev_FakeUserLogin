using AutoMapper;
using FakeUserLogin.Models;
using FakeUserLogin.ViewModels.UserViewModels;

namespace FakeUserLogin.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserViewModel, User>();
        }
    }
}

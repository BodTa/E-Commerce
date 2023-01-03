

using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Commands.DeleteUser;
using Application.Features.Users.Commands.UpdateUser;
using Application.Features.Users.Commands.UpdateUserFromAuth;
using Application.Features.Users.Dtos;
using AutoMapper;
using Core.Security.Entities;

namespace Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{
        CreateMap<User,CreatedUserDto>().ReverseMap();
        CreateMap<User,UpdatedUserDto>().ReverseMap();
        CreateMap<User,DeletedUserDto>().ReverseMap();
        CreateMap<User,UpdatedUserFromAuthDto>().ReverseMap();
        CreateMap<User,CreateUserCommand>().ReverseMap();
        CreateMap<User,UpdateUserCommand>().ReverseMap();
        CreateMap<User,DeleteUserCommand>().ReverseMap();
        CreateMap<User, UpdateUserFromAuthCommand>().ReverseMap();
        CreateMap<User,UserDto>().ReverseMap();
        CreateMap<User,UserListDto>().ReverseMap();
    }
}

using AutoMapper;
using CleanArq.Application.Users.Common.Dtos;
using CleanArq.Domain.Entities.User;

namespace CleanArq.Application.Common.Mappings;

public class UserMappings : Profile
{
	public UserMappings()
	{
		CreateMap<User, UserDto>();

		CreateMap<Address, AddressDto>();
	}
}	

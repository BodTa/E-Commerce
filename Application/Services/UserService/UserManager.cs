
using Application.Services.Repositories;
using Core.Security.Entities;

namespace Application.Services.UserService;

public class UserManager : IUserService
{

    private readonly IUserRepository _userRepository;

    public UserManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetByEmail(string email)
    {
       return await _userRepository.GetAsync(u=>u.Email == email); 
    }

    public async Task<User> GetById(int id)
    {
        return await _userRepository.GetAsync(u => u.Id == id);
    }

    public async Task<User> Update(User user)
    {
        return await _userRepository.UpdateAsync(user);
    }
}

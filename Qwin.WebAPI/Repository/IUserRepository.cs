using Domain.Dtos;

namespace Qwin.WebAPI.Repository
{
    public interface IUserRepository
    {
        Task<List<UserResponseDto>> GetUsersAsync();
    }
}

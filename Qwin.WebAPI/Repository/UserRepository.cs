using Domain.Dtos;
using Microsoft.EntityFrameworkCore;
using Qwin.WebAPI.Data;

namespace Qwin.WebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly QwinDbContext qwinDbContext;

        public UserRepository(QwinDbContext qwinDbContext)
        {
            this.qwinDbContext = qwinDbContext;
        }
        public async Task<List<UserResponseDto>> GetUsersAsync()
        {
            var usersList =await qwinDbContext.Users.Include(p => p.Details).ToListAsync();

            List<UserResponseDto> result = new List<UserResponseDto>();
            foreach (var item in usersList)
            {
                result.Add(new UserResponseDto()
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Email = item.Email,
                    Password = item.Password,
                    Address = item.Details.Address,
                    DateOfBirth = item.Details.DateOfBirth,
                    FirstName = item.Details.FirstName,
                    LastName = item.Details.LastName
                });
            }

            return result;
        }
    }
}

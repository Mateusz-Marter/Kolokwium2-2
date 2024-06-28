using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.DTO;

namespace WebApplication1.Services;

public interface IUserService
{
    Task<bool> UserExists(int newSubscriptionIdUser);
    Task<object> GetCommitments(int idUser, string orderBy);
}

public class UserService : IUserService
{
    private readonly KolosContext _context;

    public UserService(KolosContext context)
    {
        _context = context;
    }
    public async Task<bool> UserExists(int idUser)
    {
        var data = await _context.Users.CountAsync(u => u.IdUser == idUser);

        return data > 0;
    }

    public async Task<object> GetCommitments(int idUser, string orderBy)
    {
        List<UserCommitmentsDTO> userCommitments = new List<UserCommitmentsDTO>();
        
        

        return userCommitments;
    }
}
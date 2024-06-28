using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.DTO;
using WebApplication1.Models;

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
        var userCommitments = _context.Users
            .Join(_context.Subscriptions
                , u => u.IdUser
                , s => s.IdUser
                , (u, s) => u.IdUser == s.IdUser);
            //.Join(_context.Commitments, c => c);

        /*if (orderBy.Equals("PaymentDeadline"))
            await userCommitments.OrderBy(c => c.PaymentDeadline).ToListAsync();
        if(orderBy.Equals("LeftToPay"))
            await userCommitments.OrderBy(c => c.LeftToPay).ToListAsync();*/
        
        
        return userCommitments;
    }
}
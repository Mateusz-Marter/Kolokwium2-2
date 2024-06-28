using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface ISubscriptionService
{
    Task<bool> ServisExists(int newSubscriptionIdService);
    Task<bool> IsUserSubscribedToService(int newSubscriptionIdUser, int newSubscriptionIdService);
    void AddSubscribtion(NewServiceSubscription newSubscription);
}

public class SubscriptionService : ISubscriptionService
{
    private readonly KolosContext _context;

    public SubscriptionService(KolosContext context)
    {
        _context = context;
    }


    public async Task<bool> ServisExists(int idService)
    {
        var data = await _context.Services.CountAsync(s => s.IdService == idService);

        return data > 0;
    }

    public async Task<bool> IsUserSubscribedToService(int idUser, int idService)
    {
        var data = await _context.Subscriptions
            .Where(s => s.IdService == idService && s.IdUser == idUser)
            .CountAsync();

        return data > 0;
    }

    public void AddSubscribtion(NewServiceSubscription newSubscription)
    {
        var subscription = new Subscription()
        {
            IdService = newSubscription.idService,
            IdUser = newSubscription.idUser
        };

        _context.Subscriptions.Add(subscription);
        _context.SaveChangesAsync();

        int idSubscription = subscription.IdSubscription;
        DateTime paymentDeadline = DateTime.Today.AddDays(7);


        var commitment = new Commitment()
        {
            PaymentDeadline = paymentDeadline,
            LeftToPay = newSubscription.leftToPay,
            IdSubscription = idSubscription
        };

        _context.Commitments.Add(commitment);
        _context.SaveChangesAsync();
    }
}
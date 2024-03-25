using Ordering.Application.Models.Dtos;
using Ordering.Application.Contracts.Infrastructure;

namespace Ordering.Infrastructure.Providers.Email;

public class EmailService : IEmailService
{
    public async Task<bool> Send(EmailDto email)
    {
        return true;
    }
}
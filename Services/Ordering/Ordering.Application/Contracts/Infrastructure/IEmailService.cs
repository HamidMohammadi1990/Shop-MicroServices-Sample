using Ordering.Application.Models.Dtos;

namespace Ordering.Application.Contracts.Infrastructure;

public interface IEmailService
{
    Task<bool> Send(EmailDto email);
}
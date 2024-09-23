using Petalaka.Service.Shared.RabbitMQ.Events.Interfaces;

namespace Petalaka.Account.Service.Events.AccountEvent;

public class EmailVerificationEvent : IEmailVerificationEvent
{
    public string Email { get; set; }
    public string EmailOtp { get; set; }
}
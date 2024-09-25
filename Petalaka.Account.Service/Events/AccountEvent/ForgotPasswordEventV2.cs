using Petalaka.Service.Shared.RabbitMQ.Events.Interfaces;

namespace Petalaka.Account.Service.Events.AccountEvent;

public class ForgotPasswordEventV2 : IForgotPasswordEventV2
{
    public string Email { get; set; }
    public string ResetPasswordToken { get; set; }
    public string ExpiredTimeStamp { get; set; }
}
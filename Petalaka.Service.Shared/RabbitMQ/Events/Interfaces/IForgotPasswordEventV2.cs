namespace Petalaka.Service.Shared.RabbitMQ.Events.Interfaces;

public interface IForgotPasswordEventV2
{
    public string Email { get; set; }
    public string ResetPasswordToken { get; set; }
    public string ExpiredTimeStamp { get; set; }
}
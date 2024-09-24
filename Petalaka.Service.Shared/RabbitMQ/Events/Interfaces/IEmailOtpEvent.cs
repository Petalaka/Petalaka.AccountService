namespace Petalaka.Service.Shared.RabbitMQ.Events.Interfaces;

public interface IEmailOtpEvent
{
    public string Email { get; set; }
    public string EmailOtp { get; set; }
}
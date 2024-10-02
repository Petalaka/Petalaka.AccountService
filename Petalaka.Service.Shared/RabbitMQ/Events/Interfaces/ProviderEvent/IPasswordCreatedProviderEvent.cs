namespace Petalaka.Service.Shared.RabbitMQ.Events.Interfaces.ProviderEvent;

public interface IPasswordCreatedProviderEvent
{
    public string Email { get; set; }
    public string Password { get; set; }
}
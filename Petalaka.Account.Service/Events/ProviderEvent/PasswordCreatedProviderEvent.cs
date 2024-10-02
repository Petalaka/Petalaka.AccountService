using Petalaka.Service.Shared.RabbitMQ.Events.Interfaces.ProviderEvent;

namespace Petalaka.Account.Service.Events.ProviderEvent;

public class PasswordCreatedProviderEvent : IPasswordCreatedProviderEvent
{
    public string Email { get; set; }
    public string Password { get; set; }
}
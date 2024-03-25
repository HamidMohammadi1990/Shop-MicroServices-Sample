namespace EventBus.Messages.Events;

public class IntegrationBaseEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
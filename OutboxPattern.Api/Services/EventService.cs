using System.Text.Json;
using OutboxPattern.HmrcClient;
using OutboxPattern.Repository;

namespace OutboxPattern.Services;

public class EventService
{
    private readonly IHmrcClient _hmrcClient;
    private readonly OutboxRepository _outboxRepository;

    public EventService(IHmrcClient hmrcClient, OutboxRepository outboxRepository)
    {
        _hmrcClient = hmrcClient;
        _outboxRepository = outboxRepository;
    }

    public async Task PostEvent(object payload)
    {
        var now = DateTime.UtcNow;
        var outboxMessage = new OutboxMessage(
            "DummyHmrcEndpoint",
            JsonSerializer.Serialize(payload),
            Status.Open,
            now,
            now
        );
        await _outboxRepository.SaveOutboxMessage(Guid.NewGuid(), outboxMessage);
    }

    public async Task ProcessOutbox()
    {
        var items = await _outboxRepository.GetActionableItems();
        foreach (var item in items)
        {
            try
            {
                await _hmrcClient.FaultProneHmrcRequest();
                var updatedOutboxMessage = item.Value with {Status = Status.Success};
                await _outboxRepository.SaveOutboxMessage(item.Key, updatedOutboxMessage);
            }
            catch (ExternalApiException e)
            {
                var updatedOutboxMessage = item.Value with {Status = Status.Failed};
                await _outboxRepository.SaveOutboxMessage(item.Key, updatedOutboxMessage);
            }
        }
    }
}
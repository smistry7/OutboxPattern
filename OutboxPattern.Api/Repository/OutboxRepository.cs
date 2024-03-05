using Amazon.DynamoDBv2.DataModel;


namespace OutboxPattern.Repository;

public class OutboxRepository
{
    private readonly ILogger<OutboxRepository> _logger;
    private readonly Dictionary<Guid, OutboxMessage> _outbox = [];

    public OutboxRepository(ILogger<OutboxRepository> logger)
    {
        _logger = logger;
    }

    public Task SaveOutboxMessage(Guid id, OutboxMessage message)
    {
        _outbox[id] = message;
        return Task.CompletedTask;
    }

    public Task<Dictionary<Guid, OutboxMessage>> GetActionableItems(int maxToReturn = 10)
    {
        var itemsToReturn = _outbox
            .Where(x => x.Value.Status != Status.Success)
            .Take(maxToReturn)
            .ToDictionary();
        
        return Task.FromResult(itemsToReturn);
    }
}
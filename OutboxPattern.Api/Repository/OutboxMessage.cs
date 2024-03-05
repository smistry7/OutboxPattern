namespace OutboxPattern.Repository;

public record OutboxMessage(
    string Url,
    string Payload,
    Status Status,
    DateTime CreatedAt,
    DateTime UpdatedAt);
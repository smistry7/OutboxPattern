﻿namespace OutboxPattern.Controllers;

public class EventData
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;

    public string EventType { get; set; } = String.Empty;
}
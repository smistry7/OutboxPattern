using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.SQS;
using Microsoft.AspNetCore.Mvc;
using OutboxPattern.HmrcClient;
using OutboxPattern.Repository;
using OutboxPattern.Services;

namespace OutboxPattern.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
    private readonly EventService _eventService;

    public EventsController(EventService eventService)
    {
        _eventService = eventService;
    }

    [HttpPost]
    public async Task PostEvent([FromBody] EventData eventData)
    {
        await _eventService.PostEvent(eventData);
    }

    [HttpGet]
    public async Task ProcessOutbox()
    {
        await _eventService.ProcessOutbox();
    }
}
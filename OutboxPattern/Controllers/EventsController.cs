using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.SQS;
using Microsoft.AspNetCore.Mvc;

namespace OutboxPattern.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly IAmazonSQS _amazonSqs;
    private readonly string _outboxTableName;
    private readonly string _queueUrl;

    public EventsController(IDynamoDBContext dynamoDBContext, IAmazonSQS amazonSqs)
    {
        _dynamoDBContext = dynamoDBContext;
        _amazonSqs = amazonSqs;
        _outboxTableName = "YourOutboxTableName";
        _queueUrl = "http://localhost:4566/000000000000/YourQueueName";
    }

    [HttpPost]
    public async Task PostEvent([FromBody] EventData eventData)
    {
        await _dynamoDBContext.SaveAsync(typeof(EventData), eventData);
    }

    [HttpGet]
    public async Task ProcessOutbox()
    {
        
        var response = await _amazonSqs.ReceiveMessageAsync(_queueUrl);

        foreach (var item in response.Messages)
        {

        }
    }
}

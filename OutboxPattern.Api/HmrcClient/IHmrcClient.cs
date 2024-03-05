namespace OutboxPattern.HmrcClient;

public interface IHmrcClient
{
    Task FaultProneHmrcRequest();
}
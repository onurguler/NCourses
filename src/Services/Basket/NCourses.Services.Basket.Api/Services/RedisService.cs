using StackExchange.Redis;

namespace NCourses.Services.Basket.Api.Services;

public class RedisService
{
    private readonly string _host;
    private readonly int _port;

    private IConnectionMultiplexer _connectionMultiplexer;

    public RedisService(string host, int port)
    {
        _host = host;
        _port = port;
    }

    public void Connect() =>
        _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

    public IDatabase GetDb(int db = 1) => _connectionMultiplexer.GetDatabase(db);
}
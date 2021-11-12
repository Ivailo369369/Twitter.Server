namespace Twitter.Server.Infrastructure.Service
{
    using Twitter.Server.Service.ServicesType;

    public interface ICurrentUserService : IScopedService
    {
        string GetUserName();

        string GetId();
    }
}

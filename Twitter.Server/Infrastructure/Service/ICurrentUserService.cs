namespace Twitter.Server.Infrastructure.Service
{
    public interface ICurrentUserService
    {
        string GetUserName();

        string GetId();
    }
}

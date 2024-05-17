using DelugeRPCClient.Net;

namespace MediaInAction.DelugeService
{
    public interface IDelugeService
    {
        DelugeClient GetClient();
    }
}

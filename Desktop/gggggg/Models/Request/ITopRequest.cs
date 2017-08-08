namespace DXzonghejiaofei.Models.Request
{
    public interface ITopRequest
    {
        void SetChannelId(string channelId);
        void SetSecretKey(string secretKey);
        string GetApiName();
        string GetParameters();
    }
}
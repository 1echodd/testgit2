using DXzonghejiaofei.Common;
using DXzonghejiaofei.Enum;

namespace DXzonghejiaofei.Models.Request
{
    public class ResultRequest:ITopRequest
    {
        private string _channelId;
        private string _secretKey;
        public string OrderId { get; set; }
        public string State { get; set; }

        public void SetChannelId(string channelId)
        {
            _channelId=channelId;
        }

        public void SetSecretKey(string secretKey)
        {
            _secretKey=secretKey;
        }

        public string GetApiName()
        {
            return "api/Channel/Result";
        }

        public string GetParameters()
        {
            var source = $"ChannelId{_channelId}OrderId{OrderId}Secretkey{_secretKey}State{State}";
            var hmac = TopUtils.Md5Encrypt(source);
            return $"ChannelId={_channelId}&OrderId={OrderId}&State={State}&Hmac={hmac}";
        }
    }
}
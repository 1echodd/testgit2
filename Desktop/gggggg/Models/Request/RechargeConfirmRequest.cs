using DXzonghejiaofei.Common;

namespace DXzonghejiaofei.Models.Request
{
    public class RechargeConfirmRequest:ITopRequest
    {
        private string _channelId;
        private string _secretKey;
        public string OrderId { get; set; }

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
            return "api/Channel/RechargeConfirm";
        }

        public string GetParameters()
        {
            var source = $"ChannelId{_channelId}OrderId{OrderId}Secretkey{_secretKey}";
            var hmac = TopUtils.Md5Encrypt(source);
            return $"ChannelId={_channelId}&OrderId={OrderId}&Hmac={hmac}";
        }
    }
}
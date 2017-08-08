using DXzonghejiaofei.Common;
using DXzonghejiaofei.Enum;

namespace DXzonghejiaofei.Models.Request
{
    public class GetOrdersRequest :ITopRequest
    {
        private string _channelId;
        private string _secretKey;
        public string Version { get; set; }
        public GoodsType GoodsType { get; set; }

        public void SetChannelId(string channelId)
        {
            _channelId = channelId;
        }

        public void SetSecretKey(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string GetApiName()
        {
            return "api/Channel/GetOrders";
        }

        public string GetParameters()
        {
            var source =$"ChannelId{_channelId}Secretkey{_secretKey}";
            var hmac = TopUtils.Md5Encrypt(source);
            return $"ChannelId={_channelId}&Hmac={hmac}&Version={Version}&GoodsType={GoodsType}";
        }
    }
}
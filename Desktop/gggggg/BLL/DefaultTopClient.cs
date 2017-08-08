using DXzonghejiaofei.Common;
using DXzonghejiaofei.Models.Request;
using DXzonghejiaofei.Models.Response;
using Newtonsoft.Json;

namespace DXzonghejiaofei.BLL
{
    public class DefaultTopClient:ITopClient
    {
        private static string _serverUrl;
        private static string _channelId;
        private static string _secretKey;

        public static void Init(string serverUrl,string channelId,string secretKey)
        {
            _serverUrl=serverUrl;
            _channelId=channelId;
            _secretKey=secretKey;
        }

        public TopResponse<T> Execute<T>(ITopRequest request)
        {
            request.SetChannelId(_channelId);
            request.SetSecretKey(_secretKey);

            var method = request.GetApiName();
            var parameters = request.GetParameters();
            var url = $"{_serverUrl.Trim('/', '?').Trim()}/{method}";

            var response = TopUtils.PostHtml(url,parameters);
            var result = JsonConvert.DeserializeObject<TopResponse<T>>(response);
            return result;
        }
    }
}
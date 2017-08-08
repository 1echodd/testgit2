using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DXzonghejiaofei.Common
{
    public class XRequest
    {
        #region 成员字段
        private string _contentType;
        private string _userAgent;
        private string _referer;
        private bool _keepAlive;
        private Encoding _webEncoding;
        private int _timeout;
        private string _lastUrl;
        private string _webProxy;
        private CookieContainer _webCookieContainer;
        #endregion

        #region 成员属性
        /// <summary>
        /// 获取或设置 Content-typeHTTP 标头的值
        /// </summary>
        public string ContentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }

        /// <summary>
        /// 获取或设置 User-agentHTTP 标头的值
        /// </summary>
        public string UserAgent
        {
            get { return _userAgent; }
            set { _userAgent = value; }
        }

        /// <summary>
        /// 获取或设置 RefererHTTP 标头的值
        /// </summary>
        public string Referer
        {
            get { return _referer; }
            set { _referer = value; }
        }

        /// <summary>
        /// 获取或设置一个值，该值指示是否与 Internet 资源建立持久性连接
        /// </summary>
        public bool KeepAlive
        {
            get { return _keepAlive; }
            set { _keepAlive = value; }
        }

        /// <summary>
        /// 网页内容的编码方式
        /// </summary>
        public Encoding WebEncoding
        {
            get { return _webEncoding; }
            set { _webEncoding = value; }
        }

        /// <summary>
        /// 请求超时时间
        /// </summary>
        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        /// <summary>
        /// 请求到的最后的地址
        /// </summary>
        public string LastUrl
        {
            get { return _lastUrl; }
            set { _lastUrl = value; }
        }

        /// <summary>
        /// 请求使用的代理
        /// </summary>
        public string WebProxy
        {
            get { return _webProxy; }
            set { _webProxy = value; }
        }

        /// <summary>
        /// 获取或设置与此请求关联的 cookie。
        /// </summary>
        public CookieContainer WebCookieContainer
        {
            get { return _webCookieContainer; }
            set { _webCookieContainer = value; }
        }
        #endregion

        #region 构造方法
        public XRequest()
        {
            _userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; Ruibin; .NET CLR 2.0.50727; .NET4.0C; .NET4.0E; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
            _timeout = 30000;
            _webEncoding = Encoding.UTF8;
            _contentType = "application/x-www-form-urlencoded";
            _lastUrl = string.Empty;
            _webProxy = string.Empty;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
        }

        public XRequest(CookieContainer ccontainer)
            : this()
        {
            _webCookieContainer = ccontainer;
            ServicePointManager.DefaultConnectionLimit=int.MaxValue;
            ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
        }
        #endregion

        #region 成员方法
         /// <summary>
        /// Ajax请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="args"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public string AjaxHtml(string url, string args, Encoding enc)
        {
            string result = null;
            var bytes = Encoding.UTF8.GetBytes(args);

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                if (!_keepAlive)
                {
                    request.KeepAlive = false;
                    request.ProtocolVersion = HttpVersion.Version11;
                }

                request.Proxy = null;
                request.CookieContainer = _webCookieContainer;
                request.Timeout = _timeout;
                request.ReadWriteTimeout = _timeout;
                request.UserAgent = _userAgent;
                request.Referer = _referer;
                request.ContentType = _contentType;
                request.Headers.Add("no-cache: 1");
                request.Headers.Add("expire: 0");
                request.Headers.Add("Accept-Language: zh-cn");
                request.Headers.Add("Last-Modified: Thu, 1 Jan 1970 00:00:00 GMT");
                request.Accept = "*/*";
                request.ContentLength = bytes.Length;
                request.Method = "POST";

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var recvStream = response.GetResponseStream();
                if (recvStream != null)
                {
                    var reader = new StreamReader(recvStream, enc);
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                result = string.Format("Exception:{0}", ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 验证证书可用性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetHtml(string url)
        {
            return GetHtml(url, _webEncoding);
        }

        public string GetHtml(string url, WebProxy proxy)
        {
            return GetHtml(url, _webEncoding, proxy);
        }

        public string GetHtml(string url, Encoding enc, WebProxy proxy = null)
        {
            string result = null;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.CookieContainer = _webCookieContainer;
                request.Timeout = 30000;
                request.ReadWriteTimeout = 30000;
                request.UserAgent = _userAgent;
                request.Referer = _referer;
                if (proxy != null) request.Proxy = proxy;
                if (!_keepAlive)
                {
                    request.KeepAlive = false;
                    request.ProtocolVersion = HttpVersion.Version11;
                }
                var response = (HttpWebResponse)request.GetResponse();
                var recvStream=response.GetResponseStream();
                if (recvStream != null)
                {
                    var reader = new StreamReader(recvStream, enc);
                    result = reader.ReadToEnd();
                }
                _lastUrl = response.ResponseUri.ToString();
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
                result = string.Format("Exception:{0}", ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string PostHtml(string url, string args)
        {
            return PostHtml(url, args, _webEncoding);
        }

        public string PostHtml(string url, string args, WebProxy proxy)
        {
            return PostHtml(url, args, _webEncoding, proxy);
        }

        public string PostHtml(string url, string args, Encoding enc, WebProxy proxy = null)
        {
            string result = null;

            try
            {
                var bytes = enc.GetBytes(args);
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.CookieContainer = _webCookieContainer;
                request.Timeout = _timeout;
                request.ReadWriteTimeout = _timeout;
                request.UserAgent = _userAgent;
                request.Referer = _referer;
                request.ContentType = _contentType;
                request.Headers.Add("Cache-control: no-cache");
                request.Headers.Add("Accept-Language: zh-cn");
                request.Accept = "application/json, text/javascript, */*";
                request.ContentLength = bytes.Length;
                request.Method = "POST";
                request.ProtocolVersion = HttpVersion.Version11;
                if (proxy != null) request.Proxy = proxy;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
                request.ServicePoint.Address.ToString();
                var response = (HttpWebResponse)request.GetResponse();
                if (response.Cookies != null) _webCookieContainer.Add(response.Cookies);
                var recvStream =response.GetResponseStream();
                if (recvStream != null)
                {
                    var reader = new StreamReader(recvStream, enc);
                    result = reader.ReadToEnd();
                }
                _lastUrl = response.ResponseUri.ToString();
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
                result = $"Exception:{ex.Message}";
            }
            return result;
        }

        /// <summary>
        /// 请求流
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Stream GetStream(string url)
        {
            Stream recvStream = null;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.CookieContainer = _webCookieContainer;
                request.KeepAlive = false;
                request.ProtocolVersion = HttpVersion.Version11;
                request.Timeout = 30000;
                request.ReadWriteTimeout = 30000;
                request.UserAgent = _userAgent;
                request.Referer = _referer;

                var response = (HttpWebResponse)request.GetResponse();
                recvStream = response.GetResponseStream();
            }
            catch (Exception)
            {
                // ignored
            }

            return recvStream;
        }

        public Stream PostStream(string url, string args)
        {
            Stream recvStream = null;

            try
            {
                var bytes = _webEncoding.GetBytes(args);
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.CookieContainer = _webCookieContainer;
                request.KeepAlive = true;
                request.ProtocolVersion = HttpVersion.Version11;
                request.Timeout = 50000;
                request.ContentType = "application/x-www-form-urlencoded;charset=gb2312";
                request.Headers.Add("Accept-Language: zh-cn");
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.Headers.Add("Accept-Encoding: gzip, deflate");
                request.Headers.Add("Accept-Language: zh-Hans-CN,zh-Hans;q=0.5");
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko";
                request.ReadWriteTimeout = 50000;
                request.UserAgent = _userAgent;
                request.Referer = _referer;
                request.Method = "POST";

                var stream = request.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                var response = (HttpWebResponse)request.GetResponse();
                recvStream=response.GetResponseStream();
            }
            catch (Exception)
            {
                // ignored
            }

            return recvStream;
        }
        #endregion
    }
}
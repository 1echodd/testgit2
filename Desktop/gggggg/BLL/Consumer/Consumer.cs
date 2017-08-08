using System;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using DXzonghejiaofei.Common;
using DXzonghejiaofei.Models;
using DXzonghejiaofei.Models.Consumer;
using DXzonghejiaofei.Models.Response;
using Newtonsoft.Json;
using System.Web;
using System.Security.Cryptography;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using log4net;
using System.Reflection;
using System.Web.Script.Serialization;
using System.IO;
namespace DXzonghejiaofei.BLL.Consumer
{
    public class Consumer
    {       
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static UserConfig _userConfig;
        private static string _userCallBack;
        private static string _provice;
        private static FlowOrder _order;
        private static Dictionary<string, string> _codeResult;
        private static Dictionary<string, string> _submitResult;
        private readonly string ORDERDISCOUNT_URL = "/Flow/GetUserFlows";//流量包折扣
        private readonly string SUBMIT_ORDER_URL = "/FlowFixed/SubmitOrder";//只走限价接口"/FlowFixed/SubmitOrder"
        private readonly string QUERY_ORDER_URL = "/Flow/QueryOrderByCode";
        private const string QUERY_BALANCE_URL = "/Flow/GetBanance";
        private const string callbackURL= "";//http://61.162.187.40:8921/api/callback/notify

        public Consumer(FlowOrder order)
        {
            _order = order;
        }
        public static void Init(UserConfig userConfig,string userCallBack,string provice)
        {
            _userConfig = userConfig;
            _userCallBack = userCallBack;
            _provice = provice;
        }

        #region 流量包折扣
        public TopResult<GetUserFlowsResult> GetUserFlowsDis()
        {
            var result = new TopResult<GetUserFlowsResult>();
            StringBuilder contentDis = new StringBuilder();
            StringBuilder requestReportDis = new StringBuilder();
            string sign = string.Empty;
            var time = DateTime.Now.ToString("yyyyMMddHHmmss");
            contentDis.Append(_userConfig.UserName + ",");
            contentDis.Append(_userConfig.UserKey + ",");
            contentDis.Append(time);
            sign = TopUtils.Md5Encrypt(contentDis.ToString());
            requestReportDis.Append("user=").Append(_userConfig.UserName);
            requestReportDis.Append("&sign=").Append(sign);
            requestReportDis.Append("&time=").Append(time);
            //requestReportDis.Append("&isALL=").Append("true");

            string responseReport = null;
            try
            {
                responseReport = GetHtml(_userConfig.UserUrl + ORDERDISCOUNT_URL + "?" + requestReportDis.ToString());
              //  responseReport = PostHtml(_userConfig.UserUrl + ORDERDISCOUNT_URL, requestReportDis.ToString());
                result.DataView = JsonConvert.DeserializeObject<GetUserFlowsResult>(responseReport);
            }
            catch (Exception ex)
            {
                result.DataView = null;
                result.IsSuccess = false;
                result.Message = ex.Message;
                Logger.Warn(ex.ToString());
            }

            return result;
        }
        #endregion

        #region 订单提交
        public TopResult<SupInterface> SubmitOrder()
        {
            // var dataDiscountResult =GetUserFlowsDis();
            //var rechargecode = dataDiscountResult.DataView.Data;
            var result = new TopResult<SupInterface>();
            StringBuilder contentsub = new StringBuilder();
            StringBuilder requestReportsub = new StringBuilder();
            string sign = string.Empty;
            var time = DateTime.Now.ToString("yyyyMMddHHmmss");
            var timeR = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            contentsub.Append(_userConfig.UserName + ",");
            contentsub.Append(_userConfig.UserKey + ",");
            contentsub.Append(time);
            sign = TopUtils.Md5Encrypt(contentsub.ToString());
            requestReportsub.Append("user=").Append(_userConfig.UserName);
            requestReportsub.Append("&time=").Append(time);
            requestReportsub.Append("&mobile=").Append(_order.Account);
            requestReportsub.Append("&flowSize=").Append(_order.Amount);
            requestReportsub.Append("&orderCode=").Append(_order.OrderId);
            requestReportsub.Append("&local=").Append(0);
           
            //判断产品
            switch (_order.Scope) {
                case 1:
                    requestReportsub.Append("&mode=").Append(1);
                    break;
                case 0:
                    requestReportsub.Append("&mode=").Append(0);
                    break;
            }
            requestReportsub.Append("&timer=").Append("");
            requestReportsub.Append("&callback=").Append(callbackURL);
            requestReportsub.Append("&sign=").Append(sign);
            string responseReport = null;
                try{
                  responseReport = GetHtml(_userConfig.UserUrl + SUBMIT_ORDER_URL + "?" + requestReportsub.ToString());
                //responseReport = PostHtml(_userConfig.UserUrl + SUBMIT_ORDER_URL, requestReportsub.ToString());             
                    result.DataView = new SupInterface(){
                        RequestReport = _userConfig.UserUrl +SUBMIT_ORDER_URL+ requestReportsub.ToString(),
                        ResponseReport = responseReport
                    };
                }catch (Exception ex){
                    result.DataView = null;
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                    Logger.Warn(ex.ToString());
                }         
                result.IsSuccess = result.DataView != null;
                return result;
            
        }
        #endregion

        #region 订单查询
        public TopResult<QueryOrderResult> QueryOrder()
        {
            var result = new TopResult<QueryOrderResult>();
            StringBuilder contentsb = new StringBuilder();
            StringBuilder requestReportsb = new StringBuilder();
            string sign = string.Empty;
            var time = DateTime.Now.ToString("yyyyMMddHHmmss");
            contentsb.Append(_userConfig.UserName + ",");
            contentsb.Append(_userConfig.UserKey + ",");
            contentsb.Append(time);
            sign = TopUtils.Md5Encrypt(contentsb.ToString());
            requestReportsb.Append("user=").Append(_userConfig.UserName);
            requestReportsb.Append("&sign=").Append(sign);
            requestReportsb.Append("&time=").Append(time);
            requestReportsb.Append("&orderCode=").Append(_order.OrderId);//订单号

            string responseReport = null;
            try
            {
                responseReport = GetHtml(_userConfig.UserUrl + QUERY_ORDER_URL + "?" + requestReportsb.ToString());
               // responseReport = PostHtml(_userConfig.UserUrl + QUERY_ORDER_URL, requestReportsb.ToString());
               // result.Message = _codeResult[JObject.Parse(responseReport).Value<string>("errorcode")];//返回状态及错误码           
                if (responseReport!= null)
                {
                    result.DataView = JsonConvert.DeserializeObject<QueryOrderResult>(responseReport);
                }
                else{
                    result.Message = responseReport;
                }
            }
            catch (Exception ex){
                result.IsSuccess = false;
                result.DataView = null;
                result.Message = ex.Message;
                Logger.Warn(ex.ToString());
            }    
            result.IsSuccess = result.DataView != null;
            return result;
        }
        #endregion

        #region 余额查询
        public static TopResult<SupInterface> QueryBalance()
        {
            var result = new TopResult<SupInterface>();
            StringBuilder sourceString = new StringBuilder();
            StringBuilder requestReport = new StringBuilder();
            string responseReport = string.Empty;
            string sign = string.Empty;
            var time = DateTime.Now.ToString("yyyyMMddHHmmss");
            sourceString.Append(_userConfig.UserName+",");
            sourceString.Append(_userConfig.UserKey+",");
            sourceString.Append(time);
            sign = TopUtils.Md5Encrypt(sourceString.ToString());
            requestReport.Append("user=").Append(_userConfig.UserName);
            requestReport.Append("&sign=").Append(sign);
            requestReport.Append("&time=").Append(time);
            try
            {
                 responseReport = GetHtml(_userConfig.UserUrl+ QUERY_BALANCE_URL+"?"+requestReport.ToString());
                //responseReport = PostHtml(_userConfig.UserUrl.TrimEnd('/') + QUERY_BALANCE_URL, requestReport.ToString());
                result.DataView = new SupInterface()
                {
                    RequestReport = requestReport.ToString(),
                    ResponseReport = responseReport
                };
                result.Message = responseReport;
            }
            catch (Exception ex)
            {
                result.DataView = null;
                result.Message = ex.Message;
            }

            result.IsSuccess = result.DataView != null;

            return result;
        }
        #endregion

        private static string PostHtml(string url, string postData)
        {
            var data = Encoding.GetEncoding("UTF-8").GetBytes(postData);
            var webClient = new WebClient();
            webClient.Headers.Add("Accept", "*/*");
           // webClient.Headers.Add("timestamp", DateTime.Now.ToString("yyyyMMddHHmmss"));
            //webClient.Headers.Add("datatype", "json");
            webClient.Headers.Add("Content-Type", "text/plain");
            Log.Debug(url + "?" + postData);
            var response = webClient.UploadData(url, "POST", data);//得到返回字符流  
            return Encoding.GetEncoding("UTF-8").GetString(response);//解码 
        }
        private static string GetHtml(string url)
        {
            var webClient = new WebClient();
            //webClient.Headers.Add(HttpRequestHeader.KeepAlive, "False");
            webClient.Headers.Add("timestamp", DateTime.Now.ToString("yyyyMMddHHmmss"));
            webClient.Headers.Add("datatype", "json");
            webClient.Headers.Add("Content-Type", "text/plain");
            var response = webClient.DownloadData(url);//得到返回字符流  
            return Encoding.UTF8.GetString(response);//解码  
        }
    }
}
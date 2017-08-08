using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web.Http;
using DXzonghejiaofei.BLL;
using DXzonghejiaofei.Common;
using DXzonghejiaofei.Enum;
using DXzonghejiaofei.Models;
using DXzonghejiaofei.Models.Consumer;
using DXzonghejiaofei.Models.Response;
using Newtonsoft.Json;

namespace DXzonghejiaofei.Controllers
{
    public class CallbackController : ApiController
    {
        [AcceptVerbs("Get")]//"Post",
        public HttpResponseMessage Notify([FromBody] CallbackInfo callbackInfos)
        {
            try
            {
                Logger.Info($"RequstUrl:{Request.RequestUri}");
                Logger.Info($"RequstParameter:{JsonConvert.SerializeObject(callbackInfos)}");
                //Request["OrderList"];
                var state = "";
                var messages= "";
                switch (callbackInfos.Status)
                {
                    case "10":
                        state = OrderState.Success.ToString();
                        break;
                    case "1":
                        state = OrderState.Unknown.ToString();
                        break;
                    case "5":
                        state = OrderState.Fail.ToString();
                        break;
                    default:
                        state = OrderState.Unknown.ToString();
                        break;
                }

                var dealThread = new Thread(() =>
                {
                    var or = OrderManager<FlowOrder>.GetByOrderId(callbackInfos.Id);
                    if (!or.IsSuccess)
                    {
                        Logger.Info($"{callbackInfos.Id}|{or.Message}");
                    }
                    else
                    {
                        var order = or.DataView;
                        order.State = state;
                        order.OrderId = callbackInfos.Id;
                        order.Remark = messages = callbackInfos.Message;
                        TopQueue<FlowOrder>.DealOrders.Enqueue(order);
                        Logger.Info($"{order}|进入待处理订单队列");
                    }
                });
                dealThread.Start();
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString());
            }
            return new HttpResponseMessage
            {
                Content = new StringContent("1", Encoding.GetEncoding("UTF-8"), "text/plain")
            };
        }
    }
}
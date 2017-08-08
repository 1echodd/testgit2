using System;
using DXzonghejiaofei.Common;
using DXzonghejiaofei.Enum;
using DXzonghejiaofei.Models;
using DXzonghejiaofei.Models.Request;
using DXzonghejiaofei.Models.Response;

namespace DXzonghejiaofei.BLL
{
    public class Producer<T>
    {
        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="type"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static TopResult<TopResponse<GetOrdersResponse<T>>>  GetOrders(GoodsType type,string version="0.1")
        {
            var result = new TopResult<TopResponse<GetOrdersResponse<T>>>();
            var client=new DefaultTopClient();
            var request=new GetOrdersRequest()
            {
                GoodsType =type,
                Version = version
            };

            try
            {
                result.DataView = client.Execute<GetOrdersResponse<T>>(request);
                result.Message =result.DataView.Message;
                result.IsSuccess = "Success" == result.DataView.Result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                Logger.Warn(ex.ToString());
            }

            result.Message = $"获取订单操作:{result.Message}";
            return result;
        }

        /// <summary>
        /// 确认订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static TopResult<TopResponse<RechargeConfirmResponse>> RechargeConfirm(string orderId)
        {
            var result = new TopResult<TopResponse<RechargeConfirmResponse>>();
            var client = new DefaultTopClient();
            var request = new RechargeConfirmRequest
            {
                OrderId =orderId
            };

            try
            {
                result.DataView=client.Execute<RechargeConfirmResponse>(request);
                result.Message=result.DataView.Message;
                result.IsSuccess="Success"==result.DataView.Result;
            }
            catch(Exception ex)
            {
                result.IsSuccess=false;
                result.Message=ex.Message;
                Logger.Warn(ex.ToString());
            }

            result.Message=$"确认订单操作:{result.Message}";
            return result;
        }

        /// <summary>
        /// 处理订单
        /// </summary>
        /// <returns></returns>
        public static TopResult<TopResponse<ResultResponse>> Result(string orderId,string state)
        {
            var result = new TopResult<TopResponse<ResultResponse>>();
            var client = new DefaultTopClient();
            var request = new ResultRequest
            {
                OrderId=orderId,
                State = state.ToString()
            };

            try
            {
                result.DataView=client.Execute<ResultResponse>(request);
                result.Message=result.DataView.Message;
                result.IsSuccess="Success"==result.DataView.Result;
            }
            catch(Exception ex)
            {
                result.IsSuccess=false;
                result.Message=ex.Message;
                Logger.Warn(ex.ToString());
            }

            result.Message=$"处理订单操作:{result.Message}";
            return result;
        }
    }
}
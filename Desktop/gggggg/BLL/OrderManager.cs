using System;
using System.Collections.Generic;
using DXzonghejiaofei.Common;
using DXzonghejiaofei.DAL;
using DXzonghejiaofei.Models;

namespace DXzonghejiaofei.BLL
{
    public class OrderManager<T>
    {
        public static TopResult<object> Add(T order)
        {
            var result = new TopResult<object>();

            try
            {
                result.DataView = OrderService<T>.Add(order);
                result.Message="新增成功";
            }
            catch(Exception ex)
            {
                result.Message=$"新增失败:{ex.Message}";
                Logger.Warn(ex.ToString());
            }

            result.IsSuccess = result.DataView != null;
            return result;
        }

        public static TopResult<int> Update(T order)
        {
            var result = new TopResult<int>() {DataView =-1};

            try
            {
                result.DataView=OrderService<T>.Update(order);
                result.Message="更新成功";
            }
            catch(Exception ex)
            {
                result.Message=$"更新失败:{ex.Message}";
                Logger.Warn(ex.ToString());
            }

            result.IsSuccess=result.DataView!=-1;
            return result;
        }
        //获取流量产品ID
   
        public static TopResult<T> GetByshopProductId(string productName)
        {
            var result = new TopResult<T>();

            try
            {
                result.DataView= OrderService<T>.GetByshopProductId(productName);
                result.Message = "获取成功";
            }
            catch (Exception ex)
            {
                result.Message = "获取失败:{ex.Message}";
                Logger.Warn(ex.ToString());
            }

            result.IsSuccess = result.DataView != null;
            return result;
        }



        public static TopResult<T> GetByOrderId(string orderId)
        {
            var result = new TopResult<T>();

            try
            {
                result.DataView=OrderService<T>.GetByOrderId(orderId);
                result.Message="获取成功";
            }
            catch(Exception ex)
            {
                result.Message=$"获取失败:{ex.Message}";
                Logger.Warn(ex.ToString());
            }

            result.IsSuccess=result.DataView!=null;
            return result;
        }

        public static TopResult<List<T>> GetByDate(DateTime startTime,DateTime endTime)
        {
            var result = new TopResult<List<T>>();

            try
            {
                result.DataView=OrderService<T>.GetByDate(startTime,endTime);
                result.Message="查询成功";
            }
            catch(Exception ex)
            {
                result.Message=$"查询成功:{ex.Message}";
                Logger.Warn(ex.ToString());
            }

            result.IsSuccess=result.DataView!=null;
            return result;
        }

        public static TopResult<int> DeleteByDate(DateTime startTime,DateTime endTime)
        {
            var result = new TopResult<int>() {DataView = -1};

            try
            {
                result.DataView=OrderService<T>.DeleteByDate(startTime,endTime);
                result.Message="删除成功";
            }
            catch(Exception ex)
            {
                result.Message=$"删除成功:{ex.Message}";
                Logger.Warn(ex.ToString());
            }

            result.IsSuccess=result.DataView!=-1;
            return result;
        }
    }
}
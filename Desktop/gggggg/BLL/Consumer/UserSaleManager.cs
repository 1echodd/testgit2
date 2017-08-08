using System;
using System.Collections.Generic;
using DXzonghejiaofei.Common;
using DXzonghejiaofei.DAL.Consumer;
using DXzonghejiaofei.Models;
using DXzonghejiaofei.Models.Consumer;

namespace DXzonghejiaofei.BLL.Consumer
{
    public class UserSaleManager
    {
        public static TopResult<List<UserSale>> Get()
        {
            var result = new TopResult<List<UserSale>>();

            try
            {
                result.DataView=UserSaleService.Get();
                result.IsSuccess=true;
                result.Message="获取用户售价成功";
            }
            catch(Exception ex)
            {
                result.IsSuccess=false;
                result.Message=$"获取用户售价失败:{ex.Message}";
                Logger.Warn(ex.ToString());
            }

            return result;
        }

        public static TopResult<object> Add(UserSale sale)
        {
            var result = new TopResult<object>();

            try
            {
                result.DataView=UserSaleService.Add(sale);
                result.IsSuccess=true;
                result.Message="添加用户售价成功";
            }
            catch(Exception ex)
            {
                result.IsSuccess=false;
                result.Message=$"添加用户售价失败:{ex.Message}";
                Logger.Warn(ex.ToString());
            }

            return result;
        }
    }
}
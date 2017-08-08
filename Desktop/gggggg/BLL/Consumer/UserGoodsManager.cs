using System;
using System.Collections.Generic;
using DXzonghejiaofei.Common;
using DXzonghejiaofei.DAL.Consumer;
using DXzonghejiaofei.Models;
using DXzonghejiaofei.Models.Consumer;

namespace DXzonghejiaofei.BLL.Consumer
{
    public class UserGoodsManager
    {
        public static TopResult<List<UserGoods>> Get()
        {
            var result = new TopResult<List<UserGoods>>();

            try
            {
                result.DataView=UserSaleService.Get();
                result.IsSuccess=true;
                result.Message="获取产品成功";
            }
            catch(Exception ex)
            {
                result.IsSuccess=false;
                result.Message=$"获取产品失败:{ex.Message}";
                Logger.Warn(ex.ToString());
            }

            return result;
        }

        public static TopResult<object> Add(UserGoods sale)
        {
            var result = new TopResult<object>();

            try
            {
                result.DataView=UserSaleService.Add(sale);
                result.IsSuccess=true;
                result.Message= "添加产品成功";
            }
            catch(Exception ex)
            {
                result.IsSuccess=false;
                result.Message=$"添加产品失败:{ex.Message}";
                Logger.Warn(ex.ToString());
            }

            return result;
        }
    }
}
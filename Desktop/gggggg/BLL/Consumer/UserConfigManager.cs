using System;
using DXzonghejiaofei.Common;
using DXzonghejiaofei.DAL.Consumer;
using DXzonghejiaofei.Models;
using DXzonghejiaofei.Models.Consumer;

namespace DXzonghejiaofei.BLL.Consumer
{
    public class UserConfigManager
    {
        public static TopResult<string> Save(UserConfig config)
        {
            var result = new TopResult<string>();

            try
            {
                UserConfigService.Save(config);
                result.IsSuccess=true;
                result.Message="保存成功";
            }
            catch(Exception ex)
            {
                result.IsSuccess=false;
                result.Message=$"保存失败:{ex.Message}";
                Logger.Warn(ex.ToString());
            }

            return result;
        }

        public static TopResult<UserConfig> Get(long id = 1)
        {
            var result = new TopResult<UserConfig>();

            try
            {
                result.DataView=UserConfigService.Get(id);
                result.IsSuccess=true;
                result.Message="获取用户配置成功";
            }
            catch(Exception ex)
            {
                result.IsSuccess=false;
                result.Message=$"获取用户配置失败:{ex.Message}";
                Logger.Warn(ex.ToString());
            }

            return result;
        }
    }
}
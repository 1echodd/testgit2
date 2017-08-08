using System;
using DXzonghejiaofei.Common;
using DXzonghejiaofei.DAL;
using DXzonghejiaofei.Models;

namespace DXzonghejiaofei.BLL
{
    public class SystemConfigManager
    {
        public static TopResult<string> Save(SystemConfig config)
        {
            var result = new TopResult<string>();

            try
            {
                SystemConfigService.Save(config);
                result.IsSuccess = true;
                result.Message = "保存成功";
            }
            catch (Exception ex)
            {
                result.IsSuccess=false;
                result.Message=$"保存失败:{ex.Message}";
                Logger.Warn(ex.ToString());
            }

            return result;
        }

        public static TopResult<SystemConfig> Get(long id=1)
        {
            var result = new TopResult<SystemConfig>();

            try
            {
                result.DataView=SystemConfigService.Get(id);
                result.IsSuccess=true;
                result.Message="获取系统配置成功";
            }
            catch(Exception ex)
            {
                result.IsSuccess=false;
                result.Message=$"获取系统配置失败:{ex.Message}";
                Logger.Warn(ex.ToString());
            }

            return result;
        }
    }
}
using System.Reflection;
using log4net;

namespace DXzonghejiaofei.Common
{
    class Logger
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Info(string msg)
        {
            Log.Info(msg);
        }

        public static void Error(string msg)
        {
            Log.Error(msg);
        }

        public static void Warn(string msg)
        {
            Log.Warn(msg);
        }
    }
}

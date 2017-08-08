using System.Linq;
using DXzonghejiaofei.Models;
using PetaPoco;

namespace DXzonghejiaofei.DAL
{
    public class SystemConfigService
    {
        public static void Save(SystemConfig config)
        {
            using (var db = new Database())
            {
                db.Save(config);
            }
        }

        public static SystemConfig Get(long id)
        {
            using(var db = new Database())
            {
                return db.Fetch<SystemConfig>("WHERE Id=@0",id).FirstOrDefault();
            }
        }
    }
}
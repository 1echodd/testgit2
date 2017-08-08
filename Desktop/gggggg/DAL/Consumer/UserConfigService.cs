using System.Linq;
using DXzonghejiaofei.Models.Consumer;
using PetaPoco;

namespace DXzonghejiaofei.DAL.Consumer
{
    public class UserConfigService
    {
        public static void Save(UserConfig config)
        {
            using(var db = new Database())
            {
                db.Save(config);
            }
        }

        public static UserConfig Get(long id)
        {
            using(var db = new Database())
            {
                return db.Fetch<UserConfig>("WHERE Id=@0",id).FirstOrDefault();
            }
        }
    }
}
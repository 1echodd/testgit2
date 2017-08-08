using System.Collections.Generic;
using System.Linq;
using DXzonghejiaofei.Models.Consumer;
using PetaPoco;

namespace DXzonghejiaofei.DAL.Consumer
{
    public class UserSaleService
    {
        public static List<UserSale> Get()
        {
            using(var db = new Database())
            {
                return db.Fetch<UserSale>("WHERE 1=1");
            }
        }

        public static object Add(UserSale sale)
        {
            using(var db = new Database())
            {
                return db.Insert(sale);
            }
        }
    }
}
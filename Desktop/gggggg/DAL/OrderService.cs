using System;
using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using DXzonghejiaofei.Models;

namespace DXzonghejiaofei.DAL
{
    public class OrderService<T>
    {
        public static object Add(T order)
        {
            using (var db = new Database())
            {
                return db.Insert(order);
            }
        }

        public static int Update(T order)
        {
            using(var db = new Database())
            {
                return db.Update(order);
            }
        }
        //获取产品ID
         public static T GetByshopProductId(string productName)
          {
              using (var db = new Database())
              {
                var data= db.Fetch<T>("WHERE shopProductName=@0", productName).FirstOrDefault();
                return data;
              }
          }
        
        public static T GetByOrderId(string orderId)
        {
            using(var db = new Database())
            {
                return db.Fetch<T>("WHERE OrderId=@0",orderId).FirstOrDefault();
            }
        }

        public static List<T> GetByDate(DateTime startTime, DateTime endTime)
        {
            using(var db = new Database())
            {
                return db.Fetch<T>("WHERE StartTime>@0 AND EndTime<@1", startTime.ToString("yyyy-MM-dd 00:00:00"),endTime.ToString("yyyy-MM-dd 23:59:59"));
            }
        }

        public static int DeleteByDate(DateTime startTime,DateTime endTime)
        {
            using(var db = new Database())
            {
                return db.Delete<T>("WHERE StartTime>@0 AND EndTime<@1",startTime.ToString("yyyy-MM-dd 00:00:00"),endTime.ToString("yyyy-MM-dd 23:59:59"));
            }
        }
    }
}
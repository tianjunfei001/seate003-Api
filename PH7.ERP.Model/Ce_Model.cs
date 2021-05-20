using System;
using System.Collections.Generic;
using System.Text;
using ServiceStack.Redis;
using StackExchange.Redis;

namespace PH7.ERP.Model
{
    public class Ce_Model
    {
        public static string Show()
        {
            using (RedisClient redis=new RedisClient("127.0.0.1:6379"))
            {
                redis.Set<string>("name", "清华大学");
                redis.Set<string>("cmd", "8999");
                string aa= Convert.ToString(redis.Get<string>("name"));
                return aa;
            }
        }



        public static string uPDA()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");//设置连接配置，localhost也是可以的这里的端口可以自己设置为其他的等等，配置多需要自己去摸索

            IDatabase db = redis.GetDatabase();
            string value = db.StringGet("name");

            return value;    
        }




    }
}

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilOkulProc.WebApp.Extensions
{
    public static class clsExt_Session
    {
        public static void SetObject(this  ISession session, string key, object value)
        {
            string deger = JsonConvert.SerializeObject(value);
            session.SetString(key, deger);
        }

        public static T GetObject<T>(this ISession session, string key) where T:class
        {
            string deger = session.GetString(key);

            if (string.IsNullOrEmpty(deger))
            {
                return null;
            }

            T snc = JsonConvert.DeserializeObject<T>(deger);

            return snc;
            
        }
    }
}

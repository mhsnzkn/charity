using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class JsonExtension
    {
        public static T GetJsonObject<T>(string jsonString)
        {
            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}

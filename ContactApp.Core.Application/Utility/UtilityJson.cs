using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Core.Application.Utility
{
    public static class UtilityJson
    {

        static JsonSerializerSettings _Setting = new JsonSerializerSettings()
        {
            DateFormatString = "dd/MM/yyyy",
            //Converters
        };


        public static string JsonSerialize<T>(T Data)
        {

            return JsonConvert.SerializeObject(Data, Formatting.Indented, _Setting);



        }


        public static T JsonDeserialize<T>(string Data)
        {
            return !string.IsNullOrEmpty(Data) ? JsonConvert.DeserializeObject<T>(Data, _Setting) : default(T);


        }
    }

}

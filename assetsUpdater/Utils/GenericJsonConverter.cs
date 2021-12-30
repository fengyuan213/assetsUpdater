using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace assetsUpdater.Utils
{
    /*public class GenericJsonConverter<T> : JsonConverter, IBaseJsonConverter<T>
    {
        private readonly IUnityContainer Container;
        public GenericJsonConverter(IUnityContainer container)
        {
            Container = container;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(T);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var target = serializer.Deserialize<Newtonsoft.Json.Linq.JObject>(reader);
            var result = Container.Resolve<T>();
            serializer.Populate(target.CreateReader(), result);
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }*/
}

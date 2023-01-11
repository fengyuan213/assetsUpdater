using Newtonsoft.Json;

namespace assertUpdaterRefactor.Utils.JsonConverters
{
    public class ConcreteTypeConverter<TAbstract> : Newtonsoft.Json.JsonConverter
    {
        private readonly Type TReal;

        public ConcreteTypeConverter(Type type)
        {
            TReal = type;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TAbstract);
        }

        public override object? ReadJson(JsonReader reader, Type type, object? value, JsonSerializer jser)
        {
            return Convert.ChangeType(jser.Deserialize(reader, TReal), TReal);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer jser)
        {
            jser.Serialize(writer, value);
        }
    }

    public class ConcreteTypeConverter<TReal, TAbstract> : Newtonsoft.Json.JsonConverter where TReal : TAbstract
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TAbstract);
        }

        public override object? ReadJson(JsonReader reader, Type type, object? value, JsonSerializer jser)
        {
            return jser.Deserialize<TReal>(reader);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer jser)
        {
            jser.Serialize(writer, value);
        }
    }
}

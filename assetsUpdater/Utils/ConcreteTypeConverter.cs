﻿using Newtonsoft.Json;

using System;

namespace assetsUpdater.Utils
{
    public class ConcreteTypeConverter<TAbstract> : JsonConverter
    {
        private Type TReal;

        public ConcreteTypeConverter(Type type)
        {
            TReal = type;
        }

        public override Boolean CanConvert(Type objectType)
            => objectType == typeof(TAbstract);

        public override Object? ReadJson(JsonReader reader, Type type, Object? value, JsonSerializer jser)
            => Convert.ChangeType(jser.Deserialize(reader, TReal), TReal);

        public override void WriteJson(JsonWriter writer, Object? value, JsonSerializer jser)
            => jser.Serialize(writer, value);
    }

    public class ConcreteTypeConverter<TReal, TAbstract> : JsonConverter where TReal : TAbstract
    {
        public override Boolean CanConvert(Type objectType)
            => objectType == typeof(TAbstract);

        public override Object? ReadJson(JsonReader reader, Type type, Object? value, JsonSerializer jser)
            => jser.Deserialize<TReal>(reader);

        public override void WriteJson(JsonWriter writer, Object? value, JsonSerializer jser)
            => jser.Serialize(writer, value);
    }
}
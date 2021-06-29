using System;
using System.Collections.Generic;
using System.Text.Json;

namespace JsonReader
{
    public interface IMapValue<T> {
        T Map(JsonElement jsonElement);
    }

    public class MapStringValue : IMapValue<string>
    {
        public string Map(JsonElement jsonElement)
        {
            return jsonElement.GetString();
        }
    }

    public class MapIntValue : IMapValue<int>
    {
        public int Map(JsonElement jsonElement)
        {
            return jsonElement.GetInt32();
        }
    }

    public class MapDateTimeValue : IMapValue<DateTime>
    {
        public DateTime Map(JsonElement jsonElement)
        {
            return jsonElement.GetDateTime();
        }
    }

    public class Reader
    {
        private readonly JsonDocument document;
        public Reader(string json)
        {
            document = JsonDocument.Parse(json);
        }

        private JsonElement GetProperty(string[] pathToElements) {
            document.RootElement.GetProperty(pathToElements[0]);
            return document.RootElement.GetProperty(pathToElements[0]);
        }

        public int GetInt(string[] pathToElements) {
            return GetProperty(pathToElements).GetInt32();
        }

        public string GetString(string[] pathToElements) {
            return GetProperty(pathToElements).GetString();
        }

        
        public DateTime GetDateTime(string[] pathToElements) {
            return GetProperty(pathToElements).GetDateTime();
        }

        public T[] GetItems<T>(string[] pathToElements, IMapValue<T> mapItem) {
            var array = new List<T>();

            foreach(var element in GetProperty(pathToElements).EnumerateArray()) {
                array.Add(mapItem.Map(element));
            }

            return array.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text.Json;

namespace JsonReader
{
    public interface IMapItem<T> {
        T MapItem(JsonElement jsonElement);
    }

    public class MapStringItem : IMapItem<string>
    {
        public string MapItem(JsonElement jsonElement)
        {
            return jsonElement.GetString();
        }
    }

    public class MapIntItem : IMapItem<int>
    {
        public int MapItem(JsonElement jsonElement)
        {
            return jsonElement.GetInt32();
        }
    }

    public class MapDateTimeValue : IMapItem<DateTime>
    {
        public DateTime MapItem(JsonElement jsonElement)
        {
            throw new NotImplementedException();
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

        public T[] GetItems<T>(string[] pathToElements, IMapItem<T> mapItem) {
            var array = new List<T>();

            foreach(var element in GetProperty(pathToElements).EnumerateArray()) {
                array.Add(mapItem.MapItem(element));
            }

            return array.ToArray();
        }
    }
}

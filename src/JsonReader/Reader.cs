using System;
using System.Text.Json;

namespace JsonReader
{
    public class Reader
    {
        private readonly JsonDocument document;
        public Reader(string json)
        {
            document = JsonDocument.Parse(json);
        }

        private JsonElement GetProperty(string[] pathToElements) {
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
    }
}

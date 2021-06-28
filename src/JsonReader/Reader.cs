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

        public int GetInt(string[] pathToElements) {
            return document.RootElement.GetProperty(pathToElements[0]).GetInt32();
        }

        public string GetString(string[] pathToElements) {
            return document.RootElement.GetProperty(pathToElements[0]).GetString();
        }
    }
}

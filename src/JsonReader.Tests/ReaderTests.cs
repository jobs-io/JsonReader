using System;
using System.Text.Json;
using NUnit.Framework;

namespace JsonReader.Tests
{
    public class Sample {
        public string Id { get; set; }
    }

    public class MapSample : IMapValue<Sample>
    {
        private readonly string idName;
    
        public MapSample(string idName) {
            this.idName = idName;
        }

        public Sample Map(JsonElement jsonElement)
        {
            return new Sample { Id = jsonElement.GetProperty(idName).GetString() };
        }
    }

    public class ReaderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("{\"title\": \"Title\"}", new string[] {"title"}, "Title")]
        [TestCase("{\"title\": { \"shortTitle\": \"Title\" }}", new string[] {"title", "shortTitle"}, "Title")]
        public void ShouldGetStringValue(string json, string[] properties, string expectedValue)
        {
            var reader = new Reader(json);
            Assert.AreEqual(expectedValue, reader.GetString(properties));
        }

        [TestCase("{\"id\": 1}", "id", "1")]
        public void ShouldGetIntValue(string json, string property, int expectedValue)
        {
            var reader = new Reader(json);
            Assert.AreEqual(expectedValue, reader.GetInt(new string[] {property}));
        }

        [TestCase("{\"dateModified\": \"2021-06-15T09:32:25Z\"}", "dateModified", "2021/06/15 9:32:25")]
        public void ShouldGetDateTimeValue(string json, string property, DateTime expectedValue)
        {
            var reader = new Reader(json);
            Assert.AreEqual(expectedValue, reader.GetDateTime(new string[] {property}));
        }

        [TestCase("{\"items\": [\"Title\"]}", "items", new string[] { "Title" })]
        public void ShouldGetListOfStrings(string json, string property, string[] expectedValue)
        {
            var reader = new Reader(json);
            Assert.AreEqual(expectedValue, reader.GetItems<string>(new string[] {property}, new MapStringValue()));
        }

        [TestCase("{\"items\": [1]}", "items", new int[] { 1 })]
        public void ShouldGetListOfNumbers(string json, string property, int[] expectedValue)
        {
            var reader = new Reader(json);
            Assert.AreEqual(expectedValue, reader.GetItems<int>(new string[] {property}, new MapIntValue()));
        }

        [Test]
        public void ShouldGetListOfDateAndTimes()
        {
            var reader = new Reader("{\"items\": [\"2021-06-15T09:32:25Z\"]}");
            var expectedValue = new DateTime(2021, 6, 15, 9, 32, 25);
            Assert.AreEqual(new DateTime[] { expectedValue }, reader.GetItems<DateTime>(new string[] {"items"}, new MapDateTimeValue()));
        }

        [Test]
        public void ShouldGetListOfSampleClass() {
            var reader = new Reader("{\"items\": [{\"id\": \"identifier\"}]}");
            var expectedValue = new Sample[] { new Sample() { Id = "identifier" } };
            var items = reader.GetItems<Sample>(new string[] {"items"}, new MapSample("id"));

            Assert.AreEqual(expectedValue.Length, items.Length);
            Assert.AreEqual(expectedValue[0].Id, items[0].Id);
        }
    }
}

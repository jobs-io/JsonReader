using System;
using NUnit.Framework;

namespace JsonReader.Tests
{
    public class ReaderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("{\"title\": \"Title\"}", "title", "Title")]
        public void ShouldGetStringValue(string json, string property, string expectedValue)
        {

            var reader = new Reader(json);
            Assert.AreEqual(expectedValue, reader.GetString(new string[] {property}));
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
    }
}

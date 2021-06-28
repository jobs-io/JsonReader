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
    }
}

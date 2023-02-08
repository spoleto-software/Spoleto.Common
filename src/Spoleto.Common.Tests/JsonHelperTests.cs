using Spoleto.Common.Helpers;

namespace Spoleto.Common.Tests
{
    public class JsonHelperTests
    {

        [Test]
        public void StringWithCyrillic()
        {
            // Arrange
            var str = "Some Cyrillic word: 'Привет!'";

            // Act
            var json = JsonHelper.ToJson(str);
            var fromJson = JsonHelper.FromJson<string>(json);


            // Assert
            Assert.That(fromJson, Is.EqualTo(str));
            Assert.That(json.Contains("Привет"), Is.True); // Cyrillic as is.
        }
    }
}
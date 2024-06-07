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

        [Test]
        public void EnumWithJsonEnumValueAttribute()
        {
            // Arrange
            var obj = new TestClass
            {
                Test = TestEnum.Two
            };

            // Act
            var json = JsonHelper.ToJson(obj);
            var fromJson = JsonHelper.FromJson<TestClass>(json);


            // Assert
            Assert.That(fromJson.Test, Is.EqualTo(obj.Test));
            Assert.That(json.Contains("T2"), Is.True); // Cyrillic as is.
        }

        [Test]
        public void EnumWithNullJsonEnumValueAttribute()
        {
            // Arrange
            var obj = new TestClass
            {
                Test = TestEnum.Null
            };

            // Act
            var json = JsonHelper.ToJson(obj);
            var fromJson = JsonHelper.FromJson<TestClass>(json);


            // Assert
            Assert.That(fromJson.Test, Is.EqualTo(obj.Test));
            Assert.That(json.Contains("null"), Is.True); // Cyrillic as is.
        }
    }
}

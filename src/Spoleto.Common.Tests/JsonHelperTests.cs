using Spoleto.Common.Helpers;
using Spoleto.Common.Tests.Objects;

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
            Assert.Multiple(() =>
            {
                Assert.That(fromJson, Is.EqualTo(str));
                Assert.That(json.Contains("Привет"), Is.True); // Cyrillic as is.
            });
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
            Assert.Multiple(() =>
            {
                Assert.That(fromJson.Test, Is.EqualTo(obj.Test));
                Assert.That(json.Contains("T2"), Is.True);
            });
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
            Assert.Multiple(() =>
            {
                Assert.That(fromJson.Test, Is.EqualTo(obj.Test));
                Assert.That(json.Contains("null"), Is.True);
            });
        }

        [Test]
        public void EnumWithIntEnum()
        {
            // Arrange
            var obj = new TestClass
            {
                TestIntEnum = TestIntEnum.Two
            };

            // Act
            var json = JsonHelper.ToJson(obj);
            var fromJson = JsonHelper.FromJson<TestClass>(json);
                
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(fromJson.TestIntEnum, Is.EqualTo(obj.TestIntEnum));
                Assert.That(json.Contains("200"), Is.True);
                Assert.That(json.Contains("\"200\""), Is.False);
            });
        }
        [Test]
        public void EnumWithType()
        {
            // Arrange
            var systemType = typeof(int);
            var myType = typeof(TestClass);
            var obj = new TestTypeClass
            {
                MyType = myType,
                SystemType = systemType
            };

            // Act
            var json = JsonHelper.ToJson(obj);
            var fromJson = JsonHelper.FromJson<TestTypeClass>(json);

            // Assert
            Assert.Multiple(() =>
            {

                Assert.That(fromJson.MyType, Is.EqualTo(obj.MyType));
                Assert.That(fromJson.SystemType, Is.EqualTo(obj.SystemType));
            });
        }
    }
}

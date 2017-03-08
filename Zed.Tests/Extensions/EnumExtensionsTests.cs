using NUnit.Framework;
using Zed.Extensions;

namespace Zed.Tests.Extensions {

    public enum TestEnum {
        [System.ComponentModel.Description("Number one")]
        One,
        [System.ComponentModel.Description("Number two")]
        Two,
        [System.ComponentModel.Description("Number three")]
        Three,
    }

    [TestFixture]
    public class EnumExtensionsTests {
        [Test]
        public void GetDescription_SomeEnumType_EnumDescription() {
            // Arrange

            // Act
            var description = TestEnum.Two.GetEnumDescription();

            // Assert
            Assert.AreEqual("Number two", description);
        }

    }
}

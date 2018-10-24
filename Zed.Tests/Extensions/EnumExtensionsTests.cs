using NUnit.Framework;
using Zed.Data;
using Zed.DataAnnotations;
using Zed.Extensions;

namespace Zed.Tests.Extensions {

    public enum TestEnum {
        [System.ComponentModel.Description("Number one")]
        [DisplayName("ONE")]
        One,
        [DisplayName("TWO")]
        [System.ComponentModel.Description("Number two")]
        Two,
        [System.ComponentModel.Description("Number three")]
        [DisplayName("THREE")]
        Three,
    }

    [TestFixture]
    public class EnumExtensionsTests {
        [Test]
        public void GetEnumDescription_SomeEnumType_EnumDescription() {
            // Arrange

            // Act
            var description = TestEnum.Two.GetEnumDescription();

            // Assert
            Assert.AreEqual("Number two", description);
        }

        [Test]
        public void GetEnumDisplayName_SomeEnumType_EnumDisplayName() {
            // Arrange

            // Act
            var displayName = TestEnum.Three.GetEnumDisplayName();

            // Assert
            Assert.AreEqual("THREE", displayName);
        }

    }
}

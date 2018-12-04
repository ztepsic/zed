using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using Zed.Extensions;

namespace Zed.Tests.Extensions {

    public enum TestEnum {
        [Display(Description = "NUMBER ONE")]
        [System.ComponentModel.Description("Number one")]
        [Zed.DataAnnotations.DisplayName("ONE")]
        One,

        [Display(Name = "Two")]
        [Zed.DataAnnotations.DisplayName("TWO")]
        [System.ComponentModel.Description("Number two")]
        Two,

        [System.ComponentModel.Description("Number three")]
        [Zed.DataAnnotations.DisplayName("THREE")]
        [Display(Name = "Three")]
        Three,

        Four
    }

    [TestFixture]
    public class EnumExtensionsTests {

        [Test]
        public void GetEnumDescription_SomeEnumType_EnumDescriptionFromDisplayAttribute() {
            // Arrange

            // Act
            var description = TestEnum.One.GetEnumDescription();

            // Assert
            Assert.AreEqual("NUMBER ONE", description);
        }

        [Test]
        public void GetEnumDescription_SomeEnumType_EnumDescriptionFromDescriptionAttribute() {
            // Arrange

            // Act
            var description = TestEnum.Two.GetEnumDescription();

            // Assert
            Assert.AreEqual("Number two", description);
        }

        [Test]
        public void GetEnumDescription_SomeEnumTypeWithoudDecoration_EnumDescriptionAsPropertyName() {
            // Arrange

            // Act
            var description = TestEnum.Four.GetEnumDescription();

            // Assert
            Assert.AreEqual(TestEnum.Four.ToString(), description);
        }

        [Test]
        public void GetEnumDisplayName_SomeEnumType_EnumDisplayNameFromDisplayAttribute() {
            // Arrange

            // Act
            var displayName = TestEnum.Two.GetEnumDisplayName();

            // Assert
            Assert.AreEqual("Two", displayName);
        }

        [Test]
        public void GetEnumDisplayName_SomeEnumType_EnumDisplayNameFromDisplayNameAttribute() {
            // Arrange

            // Act
            var displayName = TestEnum.Three.GetEnumDisplayName();

            // Assert
            Assert.AreEqual("THREE", displayName);
        }


        [Test]
        public void GetEnumDisplayName_SomeEnumTypeWithoudDecoration_EnumDisplayNameAsPropertyName() {
            // Arrange

            // Act
            var displayName = TestEnum.Four.GetEnumDisplayName();

            // Assert
            Assert.AreEqual(TestEnum.Four.ToString(), displayName);
        }

    }
}

using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using Zed.DataAnnotations;
using Zed.Extensions;
using Zed.Tests.Extensions;

namespace Zed.Tests.DataAnnotations {

    class TestClass {
        [Display(Description = "NUMBER ONE")]
        [System.ComponentModel.Description("Number one")]
        [DisplayName("ONE")]
        public int One { get; set; } = 1;

        [Display(Name = "Two")]
        [DisplayName("TWO")]
        [System.ComponentModel.Description("Number two")]
        public int Two { get; set; } = 2;

        [System.ComponentModel.Description("Number three")]
        [DisplayName("THREE")]
        [Display(Name = "Three")]
        public int Three { get; set; } = 3;

        public int Four { get; set; } = 4;
    }

    [TestFixture]
    public class EnumExteDataAnnotationEtensionsTestsnsionsTests {

        [Test]
        public void GetPropertyDescription_SomeClassType_PropertyDescriptionFromDisplayAttribute() {
            // Arrange

            // Act
            var description = DataAnnotationExtensions.GetPropertyDescription<TestClass>(x => x.One);

            // Assert
            Assert.AreEqual("NUMBER ONE", description);
        }

        [Test]
        public void GetPropertyDescription_SomeClassType_PropertyDescriptionFromDescriptionAttribute() {
            // Arrange

            // Act
            var description = DataAnnotationExtensions.GetPropertyDescription<TestClass>(x => x.Two);

            // Assert
            Assert.AreEqual("Number two", description);
        }

        [Test]
        public void GetPropertyDescription_SomeClassTypeWithoudDecoration_PropertyDescriptionAsPropertyName() {
            // Arrange

            // Act
            var description = DataAnnotationExtensions.GetPropertyDescription<TestClass>(x => x.Four);

            // Assert
            Assert.AreEqual(TestEnum.Four.ToString(), description);
        }

        [Test]
        public void GetPropertyDisplayName_SomeClassType_PropertyDisplayNameFromDisplayAttribute() {
            // Arrange

            // Act
            var displayName = DataAnnotationExtensions.GetPropertyDisplayName<TestClass>(x => x.Two);

            // Assert
            Assert.AreEqual("Two", displayName);
        }

        [Test]
        public void GetPropertyDisplayName_SomeClassType_PropertyDisplayNameFromDisplayNameAttribute() {
            // Arrange

            // Act
            var displayName = DataAnnotationExtensions.GetPropertyDisplayName<TestClass>(x => x.Three);

            // Assert
            Assert.AreEqual("THREE", displayName);
        }

        [Test]
        public void GetPropertyDisplayName_SomeClassTypeWithoudDecoration_PropertyDisplayNameAsPropertyName() {
            // Arrange

            // Act
            var displayName = DataAnnotationExtensions.GetPropertyDisplayName<TestClass>(x => x.Four);

            // Assert
            Assert.AreEqual(nameof(TestClass.Four), displayName);
        }

        [Test]
        public void GetPropertyDescription_SomeClassInstance_PropertyDescriptionFromDisplayAttribute() {
            // Arrange
            var testInstance = new TestClass();

            // Act
            var description = testInstance.GetPropertyDescription(x => x.One);

            // Assert
            Assert.AreEqual("NUMBER ONE", description);
        }
    }
}

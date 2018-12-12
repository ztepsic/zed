using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using Zed.DataAnnotations;

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

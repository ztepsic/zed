using System.ComponentModel.DataAnnotations;
using Xunit;
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

    public class EnumExteDataAnnotationEtensionsTestsnsionsTests {

        [Fact]
        public void GetPropertyDescription_SomeClassType_PropertyDescriptionFromDisplayAttribute() {
            // Arrange

            // Act
            var description = DataAnnotationExtensions.GetPropertyDescription<TestClass>(x => x.One);

            // Assert
            Assert.Equal("NUMBER ONE", description);
        }

        [Fact]
        public void GetPropertyDescription_SomeClassType_PropertyDescriptionFromDescriptionAttribute() {
            // Arrange

            // Act
            var description = DataAnnotationExtensions.GetPropertyDescription<TestClass>(x => x.Two);

            // Assert
            Assert.Equal("Number two", description);
        }

        [Fact]
        public void GetPropertyDescription_SomeClassTypeWithoudDecoration_PropertyDescriptionAsPropertyName() {
            // Arrange

            // Act
            var description = DataAnnotationExtensions.GetPropertyDescription<TestClass>(x => x.Four);

            // Assert
            Assert.Equal(TestEnum.Four.ToString(), description);
        }

        [Fact]
        public void GetPropertyDisplayName_SomeClassType_PropertyDisplayNameFromDisplayAttribute() {
            // Arrange

            // Act
            var displayName = DataAnnotationExtensions.GetPropertyDisplayName<TestClass>(x => x.Two);

            // Assert
            Assert.Equal("Two", displayName);
        }

        [Fact]
        public void GetPropertyDisplayName_SomeClassType_PropertyDisplayNameFromDisplayNameAttribute() {
            // Arrange

            // Act
            var displayName = DataAnnotationExtensions.GetPropertyDisplayName<TestClass>(x => x.Three);

            // Assert
            Assert.Equal("THREE", displayName);
        }

        [Fact]
        public void GetPropertyDisplayName_SomeClassTypeWithoudDecoration_PropertyDisplayNameAsPropertyName() {
            // Arrange

            // Act
            var displayName = DataAnnotationExtensions.GetPropertyDisplayName<TestClass>(x => x.Four);

            // Assert
            Assert.Equal(nameof(TestClass.Four), displayName);
        }

        [Fact]
        public void GetPropertyDescription_SomeClassInstance_PropertyDescriptionFromDisplayAttribute() {
            // Arrange
            var testInstance = new TestClass();

            // Act
            var description = testInstance.GetPropertyDescription(x => x.One);

            // Assert
            Assert.Equal("NUMBER ONE", description);
        }

        [Fact]
        public void GetEnumDescription_SomeEnumType_EnumDescriptionFromDisplayAttribute() {
            // Arrange

            // Act
            var description = TestEnum.One.GetEnumDescription();

            // Assert
            Assert.Equal("NUMBER ONE", description);
        }

        [Fact]
        public void GetEnumDescription_SomeEnumType_EnumDescriptionFromDescriptionAttribute() {
            // Arrange

            // Act
            var description = TestEnum.Two.GetEnumDescription();

            // Assert
            Assert.Equal("Number two", description);
        }

        [Fact]
        public void GetEnumDescription_SomeEnumTypeWithoudDecoration_EnumDescriptionAsPropertyName() {
            // Arrange

            // Act
            var description = TestEnum.Four.GetEnumDescription();

            // Assert
            Assert.Equal(TestEnum.Four.ToString(), description);
        }

        [Fact]
        public void GetEnumDisplayName_SomeEnumType_EnumDisplayNameFromDisplayAttribute() {
            // Arrange

            // Act
            var displayName = TestEnum.Two.GetEnumDisplayName();

            // Assert
            Assert.Equal("Two", displayName);
        }

        [Fact]
        public void GetEnumDisplayName_SomeEnumType_EnumDisplayNameFromDisplayNameAttribute() {
            // Arrange

            // Act
            var displayName = TestEnum.Three.GetEnumDisplayName();

            // Assert
            Assert.Equal("THREE", displayName);
        }


        [Fact]
        public void GetEnumDisplayName_SomeEnumTypeWithoudDecoration_EnumDisplayNameAsPropertyName() {
            // Arrange

            // Act
            var displayName = TestEnum.Four.GetEnumDisplayName();

            // Assert
            Assert.Equal(TestEnum.Four.ToString(), displayName);
        }
    }
}

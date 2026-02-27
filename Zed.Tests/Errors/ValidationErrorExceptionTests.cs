using AutoFixture.Xunit2;
using Xunit;
using Zed.Errors;

namespace Zed.Tests.Errors
{
    public class ValidationErrorExceptionTests
    {

        [Theory, AutoData]
        public void Ctor_WithValidationError_SetsValidationErrorProperty(string propertyName, string message)
        {
            // Arrange
            var validationError = new ValidationError(propertyName, message);

            // Act
            var exception = new ValidationErrorException(validationError);

            // Assert
            Assert.Equal(validationError, exception.ValidationError);
        }

        [Theory, AutoData]
        public void Ctor_WithValidationError_ErrorPropertyReturnsValidationError(string propertyName, string message)
        {
            // Arrange
            var validationError = new ValidationError(propertyName, message);

            // Act
            var exception = new ValidationErrorException(validationError);

            // Assert
            Assert.Equal(validationError, exception.Error);
        }

        [Theory, AutoData]
        public void Ctor_WithValidationError_MessageMatchesValidationErrorMessage(string propertyName, string message)
        {
            // Arrange
            var validationError = new ValidationError(propertyName, message);

            // Act
            var exception = new ValidationErrorException(validationError);

            // Assert
            Assert.Equal(validationError.Message, exception.Message);
        }

        [Theory, AutoData]
        public void ValidationErrorException_IsBaseClassErrorException(string propertyName, string message)
        {
            // Arrange
            var validationError = new ValidationError(propertyName, message);

            // Act
            var exception = new ValidationErrorException(validationError);

            // Assert
            Assert.IsAssignableFrom<ErrorException>(exception);
        }

    }
}

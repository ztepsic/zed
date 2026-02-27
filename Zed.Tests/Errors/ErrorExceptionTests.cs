using AutoFixture.Xunit2;
using FluentResults;
using Xunit;
using Zed.Errors;

namespace Zed.Tests.Errors
{
    public class ErrorExceptionTests
    {

        [Theory, AutoData]
        public void Ctor_WithError_SetsErrorProperty(string message)
        {
            // Arrange
            var error = new Error(message);

            // Act
            var exception = new ErrorException(error);

            // Assert
            Assert.Equal(error, exception.Error);
        }

        [Theory, AutoData]
        public void Ctor_WithError_MessageMatchesErrorMessage(string message)
        {
            // Arrange
            var error = new Error(message);

            // Act
            var exception = new ErrorException(error);

            // Assert
            Assert.Equal(error.Message, exception.Message);
        }

        [Theory, AutoData]
        public void Ctor_WithError_IsSystemException(string message)
        {
            // Arrange
            var error = new Error(message);

            // Act
            var exception = new ErrorException(error);

            // Assert
            Assert.IsAssignableFrom<System.Exception>(exception);
        }

        [Fact]
        public void Ctor_WithErrorWithEmptyMessage_MessageIsEmpty()
        {
            // Arrange
            var error = new Error(string.Empty);

            // Act
            var exception = new ErrorException(error);

            // Assert
            Assert.Equal(string.Empty, error.Message);
            Assert.Equal(string.Empty, exception.Message);
        }

    }
}

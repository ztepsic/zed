using AutoFixture.Xunit2;
using Xunit;
using Zed.Errors;

namespace Zed.Tests.Errors
{
    public class AppErrorExceptionTests
    {

        [Theory, AutoData]
        public void Ctor_WithAppError_SetsAppErrorProperty(int code, string message)
        {
            // Arrange
            var appError = new AppError(code, message);

            // Act
            var exception = new AppErrorException(appError);

            // Assert
            Assert.Equal(appError, exception.AppError);
        }

        [Theory, AutoData]
        public void Ctor_WithAppError_ErrorPropertyReturnsAppError(int code, string message)
        {
            // Arrange
            var appError = new AppError(code, message);

            // Act
            var exception = new AppErrorException(appError);

            // Assert
            Assert.Equal(appError, exception.Error);
        }

        [Theory, AutoData]
        public void Ctor_WithAppError_MessageMatchesAppErrorMessage(int code, string message)
        {
            // Arrange
            var appError = new AppError(code, message);

            // Act
            var exception = new AppErrorException(appError);

            // Assert
            Assert.Equal(appError.Message, exception.Message);
        }

        [Theory, AutoData]
        public void AppError_IsBaseClassErrorException(int code, string message)
        {
            // Arrange
            var appError = new AppError(code, message);

            // Act
            var exception = new AppErrorException(appError);

            // Assert
            Assert.IsAssignableFrom<ErrorException>(exception);
        }

    }
}

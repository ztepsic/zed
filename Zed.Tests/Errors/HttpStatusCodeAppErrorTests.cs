using AutoFixture.Xunit2;
using System.Net;
using Xunit;
using Zed.Errors;

namespace Zed.Tests.Errors
{
    public class HttpStatusCodeAppErrorTests
    {

        [Theory, AutoData]
        public void Ctor_WithHttpStatusCode_SetsHttpStatusCodeProperty(string message)
        {
            // Act
            var error = new HttpStatusCodeAppError(HttpStatusCode.BadRequest, message);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, error.HttpStatusCode);
        }

        [Theory, AutoData]
        public void Ctor_WithHttpStatusCode_SetsCodeProperty(string message)
        {
            // Act
            var error = new HttpStatusCodeAppError(HttpStatusCode.NotFound, message);

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, error.Code);
        }

        [Theory, AutoData]
        public void Ctor_WithHttpStatusCodeAndMessage_SetsMessageProperty(string message)
        {
            // Act
            var error = new HttpStatusCodeAppError(HttpStatusCode.BadRequest, message);

            // Assert
            Assert.Equal(message, error.Message);
        }

        [Fact]
        public void Ctor_WithHttpStatusCodeOnly_SetsHttpStatusCodeProperty()
        {
            // Act
            var error = new HttpStatusCodeAppError(HttpStatusCode.InternalServerError);

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, error.HttpStatusCode);
        }

        [Fact]
        public void Ctor_WithHttpStatusCodeOnly_MessageIsNull()
        {
            // Act
            var error = new HttpStatusCodeAppError(HttpStatusCode.InternalServerError);

            // Assert
            Assert.Null(error.Message);
        }

        [Theory, AutoData]
        public void Ctor_WithHttpStatusCodeMessageAndException_SetsHttpStatusCodeProperty(string message, System.Exception causedBy)
        {
            // Act
            var error = new HttpStatusCodeAppError(HttpStatusCode.InternalServerError, message, causedBy);

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, error.HttpStatusCode);
        }

        [Theory, AutoData]
        public void BadRequest_ReturnsErrorWithBadRequestStatusCode(string message)
        {
            // Act
            var error = HttpStatusCodeAppError.BadRequest(message);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, error.HttpStatusCode);
            Assert.Equal(message, error.Message);
        }

        [Theory, AutoData]
        public void Unauthorized_ReturnsErrorWithUnauthorizedStatusCode(string message)
        {
            // Act
            var error = HttpStatusCodeAppError.Unauthorized(message);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, error.HttpStatusCode);
            Assert.Equal(message, error.Message);
        }

        [Theory, AutoData]
        public void Forbidden_ReturnsErrorWithForbiddenStatusCode(string message)
        {
            // Act
            var error = HttpStatusCodeAppError.Forbidden(message);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, error.HttpStatusCode);
            Assert.Equal(message, error.Message);
        }

        [Theory, AutoData]
        public void NotFound_ReturnsErrorWithNotFoundStatusCode(string message)
        {
            // Act
            var error = HttpStatusCodeAppError.NotFound(message);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, error.HttpStatusCode);
            Assert.Equal(message, error.Message);
        }

        [Theory, AutoData]
        public void Conflict_ReturnsErrorWithConflictStatusCode(string message)
        {
            // Act
            var error = HttpStatusCodeAppError.Conflict(message);

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, error.HttpStatusCode);
            Assert.Equal(message, error.Message);
        }

        [Theory, AutoData]
        public void UnprocessableEntity_ReturnsErrorWithUnprocessableEntityStatusCode(string message)
        {
            // Act
            var error = HttpStatusCodeAppError.UnprocessableEntity(message);

            // Assert
            Assert.Equal(HttpStatusCode.UnprocessableEntity, error.HttpStatusCode);
            Assert.Equal(message, error.Message);
        }

        [Theory, AutoData]
        public void InternalServerError_ReturnsErrorWithInternalServerErrorStatusCode(string message)
        {
            // Act
            var error = HttpStatusCodeAppError.InternalServerError(message);

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, error.HttpStatusCode);
            Assert.Equal(message, error.Message);
        }

        [Theory, AutoData]
        public void HttpStatusCodeAppError_IsAssignableFromAppError(string message)
        {
            // Act
            var error = HttpStatusCodeAppError.BadRequest(message);

            // Assert
            Assert.IsAssignableFrom<AppError>(error);
        }

    }
}

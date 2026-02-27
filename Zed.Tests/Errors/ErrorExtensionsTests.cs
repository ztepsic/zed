using AutoFixture.Xunit2;
using FluentResults;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using Xunit;
using Zed.Errors;
using Zed.Test.Xunit;

namespace Zed.Tests.Errors
{
    public class ErrorExtensionsTests
    {

        #region ToErrorException

        [Theory, AutoData]
        public void ToErrorException_WithAppError_ReturnsAppErrorException(int code, string message)
        {
            // Arrange
            var appError = new AppError(code, message);

            // Act
            var exception = appError.ToErrorException();

            // Assert
            Assert.IsType<AppErrorException>(exception);
        }

        [Theory, AutoData]
        public void ToErrorException_WithAppError_ExceptionWrapsAppError(int code, string message)
        {
            // Arrange
            var appError = new AppError(code, message);

            // Act
            var exception = (AppErrorException)appError.ToErrorException();

            // Assert
            Assert.Equal(appError, exception.AppError);
        }

        [Theory, AutoData]
        public void ToErrorException_WithValidationError_ReturnsValidationErrorException(string propertyName, string message)
        {
            // Arrange
            var validationError = new ValidationError(propertyName, message);

            // Act
            var exception = validationError.ToErrorException();

            // Assert
            Assert.IsType<ValidationErrorException>(exception);
        }

        [Theory, AutoData]
        public void ToErrorException_WithValidationError_ExceptionWrapsValidationError(string propertyName, string message)
        {
            // Arrange
            var validationError = new ValidationError(propertyName, message);

            // Act
            var exception = (ValidationErrorException)validationError.ToErrorException();

            // Assert
            Assert.Equal(validationError, exception.ValidationError);
        }

        [Theory, AutoData]
        public void ToErrorException_WithGenericError_ReturnsErrorException(string message)
        {
            // Arrange
            var error = new Error(message);

            // Act
            var exception = error.ToErrorException();

            // Assert
            Assert.IsType<ErrorException>(exception);
        }

        [Fact]
        public void ToErrorException_WithNull_ThrowsArgumentNullException()
        {
            // Arrange
            IError error = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => error.ToErrorException());
        }

        #endregion

        #region ToAppErrorException

        [Theory, AutoData]
        public void ToAppErrorException_WithAppError_ReturnsAppErrorException(int code, string message)
        {
            // Arrange
            var appError = new AppError(code, message);

            // Act
            var exception = appError.ToAppErrorException();

            // Assert
            Assert.IsType<AppErrorException>(exception);
        }

        [Theory, AutoData]
        public void ToAppErrorException_WithAppError_ExceptionWrapsAppError(int code, string message)
        {
            // Arrange
            var appError = new AppError(code, message);

            // Act
            var exception = appError.ToAppErrorException();

            // Assert
            Assert.Equal(appError, exception.AppError);
        }

        [Fact]
        public void ToAppErrorException_WithNull_ThrowsArgumentNullException()
        {
            // Arrange
            AppError appError = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => appError.ToAppErrorException());
        }

        #endregion

        #region ToValidationErrorException

        [Theory, AutoData]
        public void ToValidationErrorException_WithValidationError_ReturnsValidationErrorException(string propertyName, string message)
        {
            // Arrange
            var validationError = new ValidationError(propertyName, message);

            // Act
            var exception = validationError.ToValidationErrorException();

            // Assert
            Assert.IsType<ValidationErrorException>(exception);
        }

        [Theory, AutoData]
        public void ToValidationErrorException_WithValidationError_ExceptionWrapsValidationError(string propertyName, string message)
        {
            // Arrange
            var validationError = new ValidationError(propertyName, message);

            // Act
            var exception = validationError.ToValidationErrorException();

            // Assert
            Assert.Equal(validationError, exception.ValidationError);
        }

        [Fact]
        public void ToValidationErrorException_WithNull_ThrowsArgumentNullException()
        {
            // Arrange
            ValidationError validationError = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => validationError.ToValidationErrorException());
        }

        #endregion

        #region ToValidationErrors

        [Theory, AutoData]
        public void ToValidationErrors_WithValidationFailures_ReturnsMatchingValidationErrors(
            string propertyName1, string errorMessage1,
            string propertyName2, string errorMessage2)
        {
            // Arrange
            var failures = new List<ValidationFailure> {
                new ValidationFailure(propertyName1, errorMessage1),
                new ValidationFailure(propertyName2, errorMessage2)
            };

            // Act
            var errors = failures.ToValidationErrors();

            // Assert
            Assert.Equal(2, errors.Count);
            Assert.Equal(propertyName1, errors[0].PropertyName);
            Assert.Equal(errorMessage1, errors[0].Message);
            Assert.Equal(propertyName2, errors[1].PropertyName);
            Assert.Equal(errorMessage2, errors[1].Message);
        }

        [Fact]
        public void ToValidationErrors_WithEmptyCollection_ReturnsEmptyList()
        {
            // Arrange
            var failures = new List<ValidationFailure>();

            // Act
            var errors = failures.ToValidationErrors();

            // Assert
            Assert.Empty(errors);
        }

        [Theory, AutoData]
        public void ToValidationErrors_ReturnsValidationErrorInstances(string propertyName, string errorMessage)
        {
            // Arrange
            var failures = new List<ValidationFailure> {
                new ValidationFailure(propertyName, errorMessage)
            };

            // Act
            var errors = failures.ToValidationErrors();

            // Assert
            Assert.All(errors, e => Assert.IsType<ValidationError>(e));
        }

        #endregion

        #region ToFailResult

        [Theory, AutoData]
        public void ToFailResult_WithError_ReturnsFailedResult(string message)
        {
            // Arrange
            var error = new Error(message);

            // Act
            var result = error.ToFailResult();

            // Assert
            Assert.True(result.IsFailed);
        }

        [Theory, AutoData]
        public void ToFailResult_WithError_ResultContainsError(string message)
        {
            // Arrange
            var error = new Error(message);

            // Act
            var result = error.ToFailResult();

            // Assert
            Assert.Contains(error, result.Errors);
        }

        [Fact]
        public void ToFailResult_WithNull_ThrowsArgumentNullException()
        {
            // Arrange
            IError error = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => error.ToFailResult());
        }

        #endregion

        #region ThrowIfFailed

        [Theory, AutoData]
        public void ThrowIfFailed_WithFailedResultContainingAppError_ThrowsAppErrorException(int code, string message)
        {
            // Arrange
            var appError = new AppError(code, message);
            var result = Result.Fail(appError);

            // Act & Assert
            Assert.Throws<AppErrorException>(() => result.ThrowIfFailed());
        }

        [Theory, AutoData]
        public void ThrowIfFailed_WithFailedResultContainingValidationError_ThrowsValidationErrorException(string propertyName, string message)
        {
            // Arrange
            var validationError = new ValidationError(propertyName, message);
            var result = Result.Fail(validationError);

            // Act & Assert
            Assert.Throws<ValidationErrorException>(() => result.ThrowIfFailed());
        }

        [Theory, AutoData]
        public void ThrowIfFailed_WithFailedResultContainingGenericError_ThrowsErrorException(string message)
        {
            // Arrange
            var error = new Error(message);
            var result = Result.Fail(error);

            // Act & Assert
            Assert.Throws<ErrorException>(() => result.ThrowIfFailed());
        }

        [Fact]
        public void ThrowIfFailed_WithSuccessfulResult_DoesNotThrow()
        {
            // Arrange
            var result = Result.Ok();

            // Act & Assert
            var exception = Record.Exception(() => result.ThrowIfFailed());
            Assert.Null(exception);
        }

        [Fact]
        public void ThrowIfFailed_WithNull_ThrowsArgumentNullException()
        {
            // Arrange
            Result result = null!;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => result.ThrowIfFailed());
        }

        #endregion

    }
}

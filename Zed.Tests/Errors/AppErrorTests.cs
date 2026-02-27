using AutoFixture.Xunit2;
using FluentResults;
using System;
using Xunit;
using Zed.Errors;
using Zed.Test.Xunit;

namespace Zed.Tests.Errors
{
    public class AppErrorTests
    {

        [Fact]
        public void CodeMetadataKey_IsExpectedConstant()
        {
            Assert.Equal("Code", AppError.CodeMetadataKey);
        }

        [Theory, AutoData]
        public void Ctor_WithCode_SetsCodeProperty(int code)
        {
            // Act
            var error = new AppError(code);

            // Assert
            Assert.Equal(code, error.Code);
        }

        [Theory, AutoData]
        public void Ctor_WithCode_MessageIsNull(int code)
        {
            // Act
            var error = new AppError(code);

            // Assert
            Assert.Null(error.Message);
        }

        [Theory, AutoData]
        public void Ctor_WithCodeAndMessage_SetsCodeProperty(int code, string message)
        {
            // Act
            var error = new AppError(code, message);

            // Assert
            Assert.Equal(code, error.Code);
        }

        [Theory, AutoData]
        public void Ctor_WithCodeAndMessage_SetsMessageProperty(int code, string message)
        {
            // Act
            var error = new AppError(code, message);

            // Assert
            Assert.Equal(message, error.Message);
        }

        [Theory, AutoMockData]
        public void Ctor_WithCodeMessageAndIError_SetsCodeProperty(int code, string message, IError causedBy)
        {
            // Act
            var error = new AppError(code, message, causedBy);

            // Assert
            Assert.Equal(code, error.Code);
        }

        [Theory, AutoMockData]
        public void Ctor_WithCodeMessageAndIError_SetsMessageProperty(int code, string message, IError causedBy)
        {
            // Act
            var error = new AppError(code, message, causedBy);

            // Assert
            Assert.Equal(message, error.Message);
        }

        [Theory, AutoMockData]
        public void Ctor_WithCodeMessageAndIError_ReasonsContainsCausedBy(int code, string message, IError causedBy)
        {
            // Act
            var error = new AppError(code, message, causedBy);

            // Assert
            Assert.Contains(causedBy, error.Reasons);
        }

        [Theory, AutoData]
        public void Ctor_WithCodeMessageAndException_SetsCodeProperty(int code, string message, Exception causedBy)
        {
            // Act
            var error = new AppError(code, message, causedBy);

            // Assert
            Assert.Equal(code, error.Code);
        }

        [Theory, AutoData]
        public void Ctor_WithCodeMessageAndException_SetsMessageProperty(int code, string message, Exception causedBy)
        {
            // Act
            var error = new AppError(code, message, causedBy);

            // Assert
            Assert.Equal(message, error.Message);
        }

        [Theory, AutoData]
        public void Ctor_WithCodeMessageAndException_ReasonsContainsExceptionalError(int code, string message, Exception causedBy)
        {
            // Act
            var error = new AppError(code, message, causedBy);

            // Assert
            Assert.Single(error.Reasons);
            Assert.IsType<ExceptionalError>(error.Reasons[0]);
        }

    }
}

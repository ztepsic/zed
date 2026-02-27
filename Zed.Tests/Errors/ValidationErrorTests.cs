using AutoFixture.Xunit2;
using FluentResults;
using Xunit;
using Zed.Errors;
using Zed.Test.Xunit;

namespace Zed.Tests.Errors
{
    public class ValidationErrorTests
    {

        [Fact]
        public void PropertyNameMetadataKey_IsExpectedConstant()
        {
            Assert.Equal("PropertyName", ValidationError.PropertyNameMetadataKey);
        }

        [Theory, AutoData]
        public void Ctor_WithPropertyName_SetsPropertyNameProperty(string propertyName)
        {
            // Act
            var error = new ValidationError(propertyName);

            // Assert
            Assert.Equal(propertyName, error.PropertyName);
        }

        [Theory, AutoData]
        public void Ctor_WithPropertyName_MessageIsNull(string propertyName)
        {
            // Act
            var error = new ValidationError(propertyName);

            // Assert
            Assert.Null(error.Message);
        }

        [Theory, AutoData]
        public void Ctor_WithPropertyNameAndMessage_SetsPropertyNameProperty(string propertyName, string message)
        {
            // Act
            var error = new ValidationError(propertyName, message);

            // Assert
            Assert.Equal(propertyName, error.PropertyName);
        }

        [Theory, AutoData]
        public void Ctor_WithPropertyNameAndMessage_SetsMessageProperty(string propertyName, string message)
        {
            // Act
            var error = new ValidationError(propertyName, message);

            // Assert
            Assert.Equal(message, error.Message);
        }

        [Theory, AutoMockData]
        public void Ctor_WithPropertyNameMessageAndIError_SetsPropertyNameProperty(string propertyName, string message, IError causedBy)
        {
            // Act
            var error = new ValidationError(propertyName, message, causedBy);

            // Assert
            Assert.Equal(propertyName, error.PropertyName);
        }

        [Theory, AutoMockData]
        public void Ctor_WithPropertyNameMessageAndIError_SetsMessageProperty(string propertyName, string message, IError causedBy)
        {
            // Act
            var error = new ValidationError(propertyName, message, causedBy);

            // Assert
            Assert.Equal(message, error.Message);
        }

        [Theory, AutoMockData]
        public void Ctor_WithPropertyNameMessageAndIError_ReasonsContainsCausedBy(string propertyName, string message, IError causedBy)
        {
            // Act
            var error = new ValidationError(propertyName, message, causedBy);

            // Assert
            Assert.Contains(causedBy, error.Reasons);
        }

    }
}

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using FluentValidation;
using MediatR;
using Xunit;
using Zed.Errors;
using Zed.MediatR.Behaviors;

namespace Zed.Tests.Mediatr.Behaviors
{
    public class FluentValidationPipelineBehaviorTests
    {

        [Fact]
        public void Constructor_WithNullValidators_ThrowsArgumentNullException()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => new FluentValidationPipelineBehavior<ResultRequest, Result>(null!));

            // Assert
            Assert.Equal("validators", exception.ParamName);
        }

        [Fact]
        public async Task Handle_WithValidResultRequest_InvokesNextDelegate()
        {
            // Arrange
            var request = new ResultRequest("valid");
            var validators = new IValidator<ResultRequest>[] { new ResultRequestValidator() };
            var behavior = new FluentValidationPipelineBehavior<ResultRequest, Result>(validators);
            var nextInvocationCount = 0;
            RequestHandlerDelegate<Result> next = cancellationToken =>
            {
                nextInvocationCount++;
                return Task.FromResult(Result.Ok());
            };

            // Act
            var response = await behavior.Handle(request, next, CancellationToken.None);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(1, nextInvocationCount);
        }

        [Fact]
        public async Task Handle_WithNoValidators_InvokesNextDelegate()
        {
            // Arrange
            var request = new ResultRequest("valid");
            var validators = Array.Empty<IValidator<ResultRequest>>();
            var behavior = new FluentValidationPipelineBehavior<ResultRequest, Result>(validators);
            var nextInvocationCount = 0;
            RequestHandlerDelegate<Result> next = cancellationToken =>
            {
                nextInvocationCount++;
                return Task.FromResult(Result.Ok());
            };

            // Act
            var response = await behavior.Handle(request, next, CancellationToken.None);

            // Assert
            Assert.True(response.IsSuccess);
            Assert.Equal(1, nextInvocationCount);
        }

        [Fact]
        public async Task Handle_WithInvalidResultRequest_ReturnsFailedResultWithoutInvokingNextDelegate()
        {
            // Arrange
            var request = new ResultRequest(string.Empty);
            var validators = new IValidator<ResultRequest>[] { new ResultRequestValidator() };
            var behavior = new FluentValidationPipelineBehavior<ResultRequest, Result>(validators);
            var nextWasInvoked = false;
            RequestHandlerDelegate<Result> next = cancellationToken =>
            {
                nextWasInvoked = true;
                return Task.FromResult(Result.Ok());
            };

            // Act
            var response = await behavior.Handle(request, next, CancellationToken.None);

            // Assert
            Assert.True(response.IsFailed);
            Assert.False(nextWasInvoked);

            var validationError = Assert.IsType<ValidationError>(Assert.Single(response.Errors));
            Assert.Equal(nameof(ResultRequest.Name), validationError.PropertyName);
            Assert.Equal(ResultRequestValidator.ErrorMessage, validationError.Message);
        }

        [Fact]
        public async Task Handle_WithMultipleValidationFailures_ReturnsFailedResultContainingAllValidationErrors()
        {
            // Arrange
            var request = new ResultRequest(string.Empty);
            var validators = new IValidator<ResultRequest>[] { new ResultRequestValidator(), new SecondaryResultRequestValidator() };
            var behavior = new FluentValidationPipelineBehavior<ResultRequest, Result>(validators);

            // Act
            var response = await behavior.Handle(request, _ => Task.FromResult(Result.Ok()), CancellationToken.None);

            // Assert
            Assert.True(response.IsFailed);

            var validationErrors = response.Errors.Cast<ValidationError>().ToArray();
            Assert.Equal(2, validationErrors.Length);
            Assert.Contains(validationErrors, error => error.PropertyName == nameof(ResultRequest.Name) && error.Message == ResultRequestValidator.ErrorMessage);
            Assert.Contains(validationErrors, error => error.PropertyName == nameof(ResultRequest.Name) && error.Message == SecondaryResultRequestValidator.ErrorMessage);
        }

        [Fact]
        public async Task Handle_WithInvalidGenericResultRequest_ReturnsFailedResultWithoutInvokingNextDelegate()
        {
            // Arrange
            var request = new GenericResultRequest(string.Empty);
            var validators = new IValidator<GenericResultRequest>[] { new GenericResultRequestValidator() };
            var behavior = new FluentValidationPipelineBehavior<GenericResultRequest, Result<string>>(validators);
            var nextWasInvoked = false;
            RequestHandlerDelegate<Result<string>> next = cancellationToken =>
            {
                nextWasInvoked = true;
                return Task.FromResult(Result.Ok("value"));
            };

            // Act
            var response = await behavior.Handle(request, next, CancellationToken.None);

            // Assert
            Assert.True(response.IsFailed);
            Assert.False(nextWasInvoked);

            var validationError = Assert.IsType<ValidationError>(Assert.Single(response.Errors));
            Assert.Equal(nameof(GenericResultRequest.Name), validationError.PropertyName);
            Assert.Equal(GenericResultRequestValidator.ErrorMessage, validationError.Message);
        }

        [Fact]
        public async Task Handle_WithInvalidNonResultResponse_ThrowsValidationExceptionWithoutInvokingNextDelegate()
        {
            // Arrange
            var request = new StringResponseRequest(string.Empty);
            var validators = new IValidator<StringResponseRequest>[] { new StringResponseRequestValidator() };
            var behavior = new FluentValidationPipelineBehavior<StringResponseRequest, string>(validators);
            var nextWasInvoked = false;
            RequestHandlerDelegate<string> next = cancellationToken =>
            {
                nextWasInvoked = true;
                return Task.FromResult("value");
            };

            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => behavior.Handle(request, next, CancellationToken.None));

            // Assert
            Assert.False(nextWasInvoked);

            var failure = Assert.Single(exception.Errors);
            Assert.Equal(nameof(StringResponseRequest.Name), failure.PropertyName);
            Assert.Equal(StringResponseRequestValidator.ErrorMessage, failure.ErrorMessage);
        }

        [Fact]
        public async Task Handle_WithNullRequest_ThrowsArgumentNullException()
        {
            // Arrange
            var validators = new IValidator<ResultRequest>[] { new ResultRequestValidator() };
            var behavior = new FluentValidationPipelineBehavior<ResultRequest, Result>(validators);

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => behavior.Handle(null!, _ => Task.FromResult(Result.Ok()), CancellationToken.None));

            // Assert
            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public async Task Handle_WithNullNextDelegate_ThrowsArgumentNullException()
        {
            // Arrange
            var request = new ResultRequest("valid");
            var validators = new IValidator<ResultRequest>[] { new ResultRequestValidator() };
            var behavior = new FluentValidationPipelineBehavior<ResultRequest, Result>(validators);

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => behavior.Handle(request, null!, CancellationToken.None));

            // Assert
            Assert.Equal("next", exception.ParamName);
        }

        private sealed record ResultRequest(string Name) : IRequest<Result>;

        private sealed class ResultRequestValidator : AbstractValidator<ResultRequest>
        {
            public const string ErrorMessage = "Name is required.";

            public ResultRequestValidator()
            {
                RuleFor(request => request.Name)
                    .NotEmpty()
                    .WithMessage(ErrorMessage);
            }
        }

        private sealed class SecondaryResultRequestValidator : AbstractValidator<ResultRequest>
        {
            public const string ErrorMessage = "Name must include non-whitespace characters.";

            public SecondaryResultRequestValidator()
            {
                RuleFor(request => request.Name)
                    .Must(name => !string.IsNullOrWhiteSpace(name))
                    .WithMessage(ErrorMessage);
            }
        }

        private sealed record GenericResultRequest(string Name) : IRequest<Result<string>>;

        private sealed class GenericResultRequestValidator : AbstractValidator<GenericResultRequest>
        {
            public const string ErrorMessage = "Name is required.";

            public GenericResultRequestValidator()
            {
                RuleFor(request => request.Name)
                    .NotEmpty()
                    .WithMessage(ErrorMessage);
            }
        }

        private sealed record StringResponseRequest(string Name) : IRequest<string>;

        private sealed class StringResponseRequestValidator : AbstractValidator<StringResponseRequest>
        {
            public const string ErrorMessage = "Name is required.";

            public StringResponseRequestValidator()
            {
                RuleFor(request => request.Name)
                    .NotEmpty()
                    .WithMessage(ErrorMessage);
            }
        }
    }
}
using FluentResults;
using FluentValidation;
using MediatR;
using Zed.Errors;

namespace Zed.MediatR.Behaviors {

    /// <summary>
    /// A MediatR pipeline behavior that runs all registered <see cref="IValidator{TRequest}"/> validators
    /// for the incoming request and short-circuits the pipeline with validation errors when validation fails.
    /// </summary>
    /// <remarks>
    /// Validation failures are converted to <see cref="ValidationError"/> reasons only when
    /// <typeparamref name="TResponse"/> derives from <see cref="ResultBase"/>. Other response types
    /// receive the original <see cref="ValidationException"/>.
    /// </remarks>
    /// <typeparam name="TRequest">The type of the request being handled.</typeparam>
    /// <typeparam name="TResponse">The type of the response returned by the handler.</typeparam>
    public class FluentValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> {

        #region Fields and Properties

        /// <summary>
        /// The validators registered for <typeparamref name="TRequest"/>.
        /// </summary>
        private readonly IEnumerable<IValidator<TRequest>> validators;

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Initializes a new instance of <see cref="FluentValidationPipelineBehavior{TRequest, TResponse}"/>.
        /// </summary>
        /// <param name="validators">The validators to run against the request.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="validators"/> is <see langword="null"/>.</exception>
        public FluentValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
            => this.validators = validators ?? throw new ArgumentNullException(nameof(validators));

        #endregion

        #region Methods

        /// <summary>
        /// Validates the <paramref name="request"/> using all registered validators before invoking the next
        /// delegate in the pipeline. If validation fails and <typeparamref name="TResponse"/> is a
        /// <see cref="ResultBase"/>-derived type, a failed result containing the validation errors is returned;
        /// otherwise a <see cref="ValidationException"/> is thrown.
        /// </summary>
        /// <param name="request">The incoming request.</param>
        /// <param name="next">The delegate for the next action in the pipeline.</param>
        /// <param name="cancellationToken">A cancellation token.</param>
        /// <returns>The response from the next delegate, or a failed result when validation errors are present.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="request"/> or <paramref name="next"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ValidationException">
        /// Thrown when validation fails and <typeparamref name="TResponse"/> is not a <see cref="ResultBase"/>-derived type.
        /// </exception>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
            ArgumentNullException.ThrowIfNull(request);
            ArgumentNullException.ThrowIfNull(next);

            var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(request, cancellationToken)));
            var validationFailures = validationResults
                .SelectMany(validationResult => validationResult.Errors)
                .Where(error => error != null)
                .ToList();

            if (validationFailures.Count == 0) {
                return await next(cancellationToken);
            }

            if (!typeof(TResponse).IsValueType
                && (typeof(TResponse) == typeof(Result) || typeof(TResponse).IsSubclassOf(typeof(ResultBase)))
            ) {
                var result = Activator.CreateInstance<TResponse>();
                if (result != null) {
                    foreach (var validationError in validationFailures.ToValidationErrors()) {
                        ((ResultBase)(object)result).Reasons.Add(validationError);
                    }

                    return result;
                }
            }

            throw new ValidationException(validationFailures);
        }

        #endregion
    }
}

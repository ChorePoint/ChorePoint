using FluentValidation;
using FluentValidation.Results;

using MediatR;

namespace ChorePoint.Application.Behaviours;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        ValidationContext<TRequest> context = new(request);

        List<ValidationFailure> failures = validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(vf => vf is not null)
            .ToList();

        if (failures.Count != 0)
        {
            throw new ValidationException(failures);
        }

        return await next(cancellationToken);
    }
}

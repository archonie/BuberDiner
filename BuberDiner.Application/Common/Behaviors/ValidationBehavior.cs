using BuberDiner.Application.Authentication.Commands.Register;
using BuberDiner.Application.Authentication.Common;
using BuberDiner.Domain.Entities;
using FluentValidation;
using MediatR;

namespace BuberDiner.Application.Common.Behaviors;

public class ValidationBehavior: IPipelineBehavior<RegisterCommand, AuthenticationResult>
{
    private readonly IValidator<RegisterCommand> _validator;

    public ValidationBehavior(IValidator<RegisterCommand> validator)
    {
        _validator = validator;
    }

    public async Task<AuthenticationResult> Handle(
        RegisterCommand request, 
        RequestHandlerDelegate<AuthenticationResult> next, 
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid)
        {
            return await next();
        }
        var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
        return new AuthenticationResult(new User(), Token: string.Empty);
    }
}
using BuberDiner.Application.Authentication.Common;
using MediatR;

namespace BuberDiner.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
    ): IRequest<AuthenticationResult>;
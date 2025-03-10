using BuberDiner.Application.Authentication.Common;
using MediatR;

namespace BuberDiner.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password): IRequest<AuthenticationResult>;
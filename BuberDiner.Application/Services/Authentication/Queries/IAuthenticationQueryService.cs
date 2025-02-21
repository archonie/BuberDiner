using BuberDiner.Application.Services.Authentication.Common;

namespace BuberDiner.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
    AuthenticationResult Login(string email, string password);
}
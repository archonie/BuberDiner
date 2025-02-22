using BuberDiner.Application.Authentication.Common;
using BuberDiner.Application.Common.Interfaces.Authentication;
using BuberDiner.Application.Common.Interfaces.Persistence;
using BuberDiner.Domain.Entities;
using MediatR;

namespace BuberDiner.Application.Authentication.Queries.Login;

public class LoginQueryHandler: IRequestHandler<LoginQuery, AuthenticationResult> 
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }


    public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (_userRepository.GetUserByEmail(request.Email) is not User user)
        {
            throw new Exception("User with given email does not exist."); 
        }
        
        // 2. Validate the password is correct 
        if (user.Password != request.Password)
        {
            throw new Exception("Invalid password.");
        }
        
        
        // 3. Crete JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user,
            token
        );
    }
}
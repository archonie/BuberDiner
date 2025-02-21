using BuberDiner.Application.Common.Interfaces.Authentication;
using BuberDiner.Application.Common.Interfaces.Persistence;
using BuberDiner.Domain.Entities;

namespace BuberDiner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{

    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string email, string password)
    {
        // 1. Validate the user exists
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User with given email does not exist."); 
        }
        
        // 2. Validate the password is correct 
        if (user.Password != password)
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

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // 1. Validate the user doesn't exist
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            throw new Exception("Email address already exists.");
        }
         
        // 2. Create user (generate unique Id) && Persist to DB
        var user = new User
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Password = password
        };
        _userRepository.Add(user);
        // 3. Create JWT Token
        
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user,
            token
            );
    }
}
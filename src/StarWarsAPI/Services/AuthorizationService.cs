using Microsoft.EntityFrameworkCore;
using StarWarsAPI.Common;
using StarWarsAPI.Context.Abstraction;
using StarWarsAPI.Entities;

namespace StarWarsAPI.Services;

public class AuthorizationService
{
    private readonly IDatabaseContext _databaseContext;
    private readonly JwtHelper _jwtHelper;

    public AuthorizationService(IDatabaseContext databaseContext, JwtHelper jwtHelper)
    {
        _databaseContext = databaseContext;
        _jwtHelper = jwtHelper;
    }

    public async Task<AuthorizationResult> Login(string email, string password)
    {
        var authorizationResult = new AuthorizationResult();
        var passwordHash = ComputeHash256.ComputeSha256Hash(password);
        var user = await _databaseContext.Users
            .FirstOrDefaultAsync(p => p.Email == email && p.Password == passwordHash);

        if (user is null)
        {
            authorizationResult.Failed("Неправильный логин или пароль");
            return authorizationResult;
        }
        
        authorizationResult.Token = _jwtHelper.GenerateJwt(user);
        
        return authorizationResult;
    }

    public async Task<AuthorizationResult> Register(string email, string password)
    {
        var authorizationResult = new AuthorizationResult();
        var passwordHash = ComputeHash256.ComputeSha256Hash(password);
        var user = await _databaseContext.Users
            .FirstOrDefaultAsync(p => p.Email == email);

        if (user is not null)
        {
            authorizationResult.Failed("Такой пользователь уже зарегестрирован");
            return authorizationResult;
        }

        var createUser = await _databaseContext.Users.AddAsync(new User
        {
            Email = email,
            Password = passwordHash
        });

        await _databaseContext.SaveChangesAsync();
        
        authorizationResult.Token = _jwtHelper.GenerateJwt(createUser.Entity);

        return authorizationResult;
    }
    
    
}
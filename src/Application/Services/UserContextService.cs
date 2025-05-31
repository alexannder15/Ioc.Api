using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class UserContextService(
    IHttpContextAccessor httpContextAccessor,
    UserManager<User> userManager
) : IUserContextService
{
    private User? _currentUser;

    public async Task<User> GetCurrentUser()
    {
        if (_currentUser != null)
            return _currentUser;

        //check while request is made by a background (schedule) task. DO NOT REMOVE
        if (httpContextAccessor.HttpContext == null)
            _currentUser = await userManager.FindByNameAsync("alexannder15@hotmail.com");
        else
        {
            var contextUser = httpContextAccessor.HttpContext.User;
            _currentUser = await userManager.GetUserAsync(contextUser);
        }

        if (_currentUser != null)
            return _currentUser;

        return _currentUser;
    }
}

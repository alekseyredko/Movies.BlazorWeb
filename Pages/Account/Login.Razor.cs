using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Movies.Data.Results;
using Movies.Infrastructure.Models;
using Movies.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.BlazorWeb.Pages.Account
{
    public partial class Login
    {
        [Inject]
        ICustomAuthentication customAuthentication { get; set; }
        
        [Inject]
        NavigationManager NavigationManager { get; set; }

        private string login { get; set; }
        private string password { get; set; }

        private Result<LoginUserResponse> response;

        [CascadingParameter] 
        Task<AuthenticationState> authenticationStateTask { get; set; }

        private async Task LogUsername()
        {
            await authenticationStateTask;

            var userRequest = new LoginUserRequest()
            {
                Login = login,
                Password = password
            };
            response = await customAuthentication.TryLoginAsync(userRequest);
            if (response.ResultType == ResultType.Ok)
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Components;
using Movies.Data.Results;
using Movies.Infrastructure.Models;
using Movies.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.BlazorWeb.Pages.Account
{
    public partial class Register
    {
        [Inject]
        private ICustomAuthentication _authentication { get; set; }
        
        [Inject]
        private NavigationManager _navigationManager  { get; set; }
        
        [Inject]
        private IMapper _mapper { get; set; }

        private RegisterUserRequest registerUserRequest { get; set; }        

        private Result<RegisterUserResponse> result;

        protected override Task OnParametersSetAsync()
        {
            registerUserRequest = new RegisterUserRequest();
            return base.OnParametersSetAsync();
        }

        private async Task TryRegisterAsync()
        {           
            result = await _authentication.TryRegisterAsync(registerUserRequest);

            if (result.ResultType == ResultType.Ok)
            {                
                _navigationManager.NavigateTo("/");
            }
        }
    }
}

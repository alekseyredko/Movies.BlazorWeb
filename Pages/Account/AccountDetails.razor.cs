using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server;
using Movies.Data.Results;
using Movies.Data.Services.Interfaces;
using Movies.Infrastructure.Models.Producer;
using Movies.Infrastructure.Models.User;
using Movies.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.BlazorWeb.Pages.Account
{
    public partial class AccountDetails
    {
        [Inject]
        private IUserService _userService { get; set; }

        [Inject]
        private ICustomAuthentication _authentication { get; set; }

        [Inject]
        private ServerAuthenticationStateProvider _authenticationProvider { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }


        private string country { get; set; }
        private Result<GetUserResponse> user { get; set; }
        private Result<ProducerResponse> producer { get; set; }

        protected override async Task OnInitializedAsync()
        {

            user = await _authentication.GetCurrentUserDataAsync();

        }

        private async Task RegisterAsProducer()
        {
            var producerRequest = new ProducerRequest
            {
                Country = country
            };

            producer = await _authentication.TryRegisterAsProducerAsync(producerRequest);

            if (producer.ResultType == ResultType.Ok)
            {
                NavigationManager.NavigateTo("/");
            }
        }

        private async Task RegisterAsReviewer()
        {
            
        }

    }
}

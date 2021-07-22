using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.BlazorWeb.Pages.Account
{
    public partial class Logout
    {
        protected override async Task OnInitializedAsync()
        {
            await customAuthentication.LogoutAsync();

            navigationManager.NavigateTo("/");
        }
    }
}

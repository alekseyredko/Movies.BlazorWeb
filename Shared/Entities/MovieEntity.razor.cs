using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Movies.Data.Models;
using Movies.Data.Results;
using Movies.Data.Services.Interfaces;
using Movies.Infrastructure.Models.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Movies.BlazorWeb.Shared.Entities
{
    public partial class MovieEntity: ComponentBase
    {        
        [Inject]
        NavigationManager navigationManager { get; set; }

        [Parameter]
        public EventCallback<int> OnMovieDeleted { get; set; }

        [Parameter]
        public MovieResponse Movie { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        
        private bool canEditAndDelete { get; set; }

        private bool deleteDialogOpen { get; set; }

        private int userId { get; set; }

        private string movieLink { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var state = await authenticationStateTask;

            if (state.User.Identity.IsAuthenticated)
            {
                var claim = state.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (claim != null)
                {
                    userId = int.Parse(claim.Value);

                    if (state.User.IsInRole(Enum.GetName(UserRoles.Producer)) && Movie.ProducerId == userId)
                    {
                        canEditAndDelete = true;
                    }
                }   
            }

            movieLink = $"/movies/{Movie.MovieId}";

            await base.OnParametersSetAsync();
        }

        private void ShowDeleteDialog()
        {            
            deleteDialogOpen = true;
            this.StateHasChanged();
        }

        private void GoToEditMovie()
        {
            navigationManager.NavigateTo($"/movies/{Movie.MovieId}/edit");
        }

        private async Task OnDeleteAsync(bool confirm)
        {
            if (confirm)
            {                
                await OnMovieDeleted.InvokeAsync(Movie.MovieId);

                this.StateHasChanged();
            }
            deleteDialogOpen = false;
        }
    }
}

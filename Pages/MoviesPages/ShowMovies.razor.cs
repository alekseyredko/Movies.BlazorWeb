using AutoMapper;
using Microsoft.AspNetCore.Components;
using Movies.Data.Models;
using Movies.Data.Results;
using Movies.Data.Services.Interfaces;
using Movies.Infrastructure.Models.Movie;
using Movies.Infrastructure.Models.User;
using Movies.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.BlazorWeb.Pages.MoviesPages
{    
    public partial class ShowMovies
    {
        private Result<IEnumerable<Movie>> movies;
        private IEnumerable<Movie> moviesToShow;

        private Result<GetUserResponse> currentUser;
                                       
        [Inject]
        private ICustomAuthentication customAuthentication { get; set; }

        [Inject]
        private IMovieService movieService { get; set; }

        private bool showOnlyMyMovies { get; set; }

        private bool deleteDialogOpen { get; set; }

        private int movieIdToDelete { get; set; }

        private bool canShowEdit { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadMoviesAsync(true, false);

            currentUser = await customAuthentication.GetCurrentUserDataAsync();
            
            if (currentUser.ResultType == ResultType.Ok)
            {
                canShowEdit = currentUser.Value.Roles.Contains(UserRoles.Producer);
            }
        }
      
        private async Task LoadMoviesAsync(bool shouldLoad, bool showOnlyMyMovies)
        {
            if (shouldLoad)
            {
                movies = await movieService.GetAllMoviesAsync();
            }

            if (showOnlyMyMovies)
            {
                moviesToShow = movies.Value.Where(x => x.ProducerId == currentUser.Value.UserId);
            }
            else
            {
                moviesToShow = movies.Value;
            }
        }

        private async Task OnShowInlyMyMoviesAsync(ChangeEventArgs e)
        {
            showOnlyMyMovies = (bool)e.Value;
            await LoadMoviesAsync(false, showOnlyMyMovies);
        }

        private void ShowDeleteDialog(int movieId)
        {
            movieIdToDelete = movieId;
            deleteDialogOpen = true;
            this.StateHasChanged();
        }

        private async Task OnDeleteAsync(bool confirm)
        {
            if (confirm)
            {
                var response = await movieService.DeleteMovieAsync(currentUser.Value.UserId, movieIdToDelete);

                if (response.ResultType == ResultType.Ok)
                {
                    await LoadMoviesAsync(true, showOnlyMyMovies);
                }

                this.StateHasChanged();
            }
            deleteDialogOpen = false;
        }
    }
}

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
        
        private bool canShowEdit;
        
        private RenderFragment<Movie> renderFragment;
                

        [Inject]
        private ICustomAuthentication customAuthentication { get; set; }

        [Inject]
        private IMovieService movieService { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }        

        [Inject]
        private IMapper mapper { get; set; }

        protected override async Task OnInitializedAsync()
        {
            movies = await movieService.GetAllMoviesAsync();

            moviesToShow = movies.Value;

            currentUser = await customAuthentication.GetCurrentUserDataAsync();
            
            if (currentUser.ResultType == ResultType.Ok)
            {
                canShowEdit = currentUser.Value.Roles.Contains(UserRoles.Producer);
            }
        }
      
        private async Task ChangeMovieShowAsync(ChangeEventArgs e)
        {           
            if ((bool)e.Value)
            {
                moviesToShow = movies.Value.Where(x => x.ProducerId == currentUser.Value.UserId);
            }
            else
            {
                moviesToShow = movies.Value;
            }
        }
    }
}

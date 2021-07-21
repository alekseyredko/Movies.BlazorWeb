using Microsoft.AspNetCore.Components;
using Movies.Data.Models;
using Movies.Data.Results;
using Movies.Data.Services.Interfaces;
using Movies.Infrastructure.Models.User;
using Movies.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.BlazorWeb.Pages.MoviesPages
{    
    public partial class ShowMovies: ComponentBase
    {
        private Result<IEnumerable<Movie>> _movies;
        private Result<GetUserResponse> _currentUser;
        private bool _canShowEdit;        

        [Inject]
        private ICustomAuthentication _customAuthentication { get; set; }

        [Inject]
        private IMovieService _movieService { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }        

        protected override async Task OnInitializedAsync()
        {
            _movies = await _movieService.GetAllMoviesAsync();
            _currentUser = await _customAuthentication.GetCurrentUserDataAsync();
            
            if (_currentUser.ResultType == ResultType.Ok)
            {
                _canShowEdit = _currentUser.Value.Roles.Contains(UserRoles.Producer);
            }
        }
    }
}

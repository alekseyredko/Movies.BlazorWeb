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
    public partial class ShowMovies: ComponentBase
    {
        private Result<IEnumerable<Movie>> _movies;
        private IEnumerable<Movie> _moviesToShow;

        private Result<GetUserResponse> _currentUser;
        
        private bool _canShowEdit;
        
        private RenderFragment<Movie> renderFragment;
                

        [Inject]
        private ICustomAuthentication _customAuthentication { get; set; }

        [Inject]
        private IMovieService _movieService { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }        

        [Inject]
        private IMapper _mapper { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _movies = await _movieService.GetAllMoviesAsync();

            _moviesToShow = _movies.Value;

            _currentUser = await _customAuthentication.GetCurrentUserDataAsync();
            
            if (_currentUser.ResultType == ResultType.Ok)
            {
                _canShowEdit = _currentUser.Value.Roles.Contains(UserRoles.Producer);
            }
        }

       

        private async Task ChangeMovieShowAsync(ChangeEventArgs e)
        {           
            if ((bool)e.Value)
            {
                _moviesToShow = _movies.Value.Where(x => x.ProducerId == _currentUser.Value.UserId);
            }
            else
            {
                _moviesToShow = _movies.Value;
            }
        }
    }
}

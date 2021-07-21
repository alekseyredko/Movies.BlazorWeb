using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Producer")]
    public partial class EditMovie
    {
        [Inject]
        private IMovieService _movieService { get; set; }

        [Inject]
        private ICustomAuthentication _customAuthentication { get; set; }

        [Inject]
        private IMapper _mapper { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Parameter]
        public int Id { get; set; }

        private string movieName;

        private string duration;

        private Result<GetUserResponse> _currentUser;

        private Result<MovieResponse> result;

        private Result<MovieResponse> updateResult;

        protected override async Task OnParametersSetAsync()
        {
            var movie = await _movieService.GetMovieAsync(Id);

            result = _mapper.Map<Result<Movie>, Result<MovieResponse>>(movie);
            _currentUser = await _customAuthentication.GetCurrentUserDataAsync();

            if (result.ResultType == ResultType.Ok)
            {
                movieName = result.Value.MovieName;
                duration = result.Value.Duration.ToString();
            }

            await base.OnParametersSetAsync();
        }

        private async Task UpdateMovieAsync()
        {
            if (TimeSpan.TryParse(duration, out TimeSpan timeSpan))
            {
                var request = new Movie
                {
                    MovieName = movieName,
                    Duration = timeSpan,
                    ProducerId = _currentUser.Value.UserId
                };

                var response = await _movieService.UpdateMovieAsync(_currentUser.Value.UserId, Id, request);

                updateResult = _mapper.Map<Result<Movie>, Result<MovieResponse>>(response);

                if (updateResult.ResultType == ResultType.Ok)
                {
                    _navigationManager.NavigateTo($"movies/{updateResult.Value.MovieId}");
                }
            }

        }
    }
}

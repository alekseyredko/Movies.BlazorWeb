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
    public partial class ShowMovie
    {
        [Inject]
        private IMovieService _movieService { get; set; }
        
        [Inject]
        private IReviewService _reviewService { get; set; }
        
        [Inject]
        private ICustomAuthentication _customAuthentication { get; set; }


        [Parameter]
        public int Id { get; set; }


        private Result<GetUserResponse> _currentUser;
        private Result<Movie> _movie;
        private string link;


        protected override async Task OnInitializedAsync()
        {
            _movie = await _movieService.GetMovieAsync(Id);
            _currentUser = await _customAuthentication.GetCurrentUserDataAsync();

            await base.OnInitializedAsync();

            link = $"/movies/{Id}/edit";
        }
    }
}

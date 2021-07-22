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
        private IMovieService movieService { get; set; }
        
        [Inject]
        private IReviewService reviewService { get; set; }
        
        [Inject]
        private ICustomAuthentication customAuthentication { get; set; }


        [Parameter]
        public int Id { get; set; }


        private Result<GetUserResponse> currentUser;
        private Result<Movie> movie;
        private string link;


        protected override async Task OnParametersSetAsync()
        {
            movie = await movieService.GetMovieAsync(Id);
            currentUser = await customAuthentication.GetCurrentUserDataAsync();

            await base.OnInitializedAsync();

            link = $"/movies/{Id}/edit";
        }
    }
}

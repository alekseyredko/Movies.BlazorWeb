using AutoMapper;
using Microsoft.AspNetCore.Components;
using Movies.Data.Models;
using Movies.Data.Results;
using Movies.Data.Services.Interfaces;
using Movies.Infrastructure.Models.Review;
using Movies.Infrastructure.Models.User;
using Movies.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.BlazorWeb.Pages.Reviews
{    
    public partial class ShowReviews
    {
        [Inject]
        private IReviewService reviewService { get; set; }

        [Inject]
        private ICustomAuthentication customAuthentication { get; set; }

        [Inject]
        private IMovieService movieService { get; set; }

        [Inject]
        private IMapper mapper { get; set; }

        private Result<IEnumerable<ReviewResponse>> reviews { get; set; }
        private Result<GetUserResponse> currentUser;

        [Parameter]
        public int? MovieId { get; set; }

        [Parameter]
        public int? ReviewerId { get; set; }

        protected override async Task OnParametersSetAsync()
        {            
            var getReviews = new Result<IEnumerable<Review>>();

            if (MovieId.HasValue && ReviewerId.HasValue)
            {
                getReviews = await reviewService.GetMovieReviewsAsync(MovieId.Value);
                getReviews.Value = getReviews.Value.Where(x => x.ReviewerId == ReviewerId);
            }
            else if (MovieId.HasValue)
            {
                getReviews = await reviewService.GetMovieReviewsAsync(MovieId.Value);
            }
            else if (ReviewerId.HasValue)
            {
                getReviews = await reviewService.GetReviewerReviewsAsync(ReviewerId.Value);
            }
            else
            {
                getReviews = await reviewService.GetAllReviewsAsync();
            }


            reviews = mapper.Map<Result<IEnumerable<ReviewResponse>>>(getReviews);

            currentUser = await customAuthentication.GetCurrentUserDataAsync();

            await base.OnInitializedAsync();
        }
    }
}

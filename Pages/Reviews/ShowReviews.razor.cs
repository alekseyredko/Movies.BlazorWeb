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
        private Result<GetUserResponse> currentUser { get; set; }
      

        protected override async Task OnParametersSetAsync()
        {
            await LoadReviewsAsync(false);

            currentUser = await customAuthentication.GetCurrentUserDataAsync();

            await base.OnInitializedAsync();
        }

        private async Task LoadReviewsAsync(bool showOnlyMyReviews)
        {
            var getReviews = new Result<IEnumerable<Review>>();            
            if (showOnlyMyReviews)
            {
                getReviews = await reviewService.GetAllReviewsAsync();
            }
            else
            {
                getReviews = await reviewService.GetAllReviewsAsync();
            }
            reviews = mapper.Map<Result<IEnumerable<Review>>, Result<IEnumerable<ReviewResponse>>>(getReviews);
        }

        private async Task OnDelete(ReviewResponse response)
        {
            var result = await reviewService.DeleteReviewAsync(currentUser.Value.UserId, response.ReviewId);
            if (result.ResultType == ResultType.Ok)
            {
                await LoadReviewsAsync(false);
            }
        }

        private async Task OnUpdate(ReviewResponse response)
        {
            
        }
    }
}

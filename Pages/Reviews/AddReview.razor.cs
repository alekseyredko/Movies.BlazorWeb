using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Reviewer")]
    public partial class AddReview
    {
        [Inject]
        private ICustomAuthentication authentication { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private IReviewService reviewService { get; set; }

        [Inject]
        private IMapper mapper { get; set; }

        [Parameter]
        public int MovieId { get; set; }      
        
        private ReviewRequest reviewRequest { get; set; }        
        private Result<ReviewResponse> result { get; set; }
        private Result<GetUserResponse> currentUser { get; set; }
        
        protected override async Task OnParametersSetAsync()
        {
            reviewRequest = new ReviewRequest();

            currentUser = await authentication.GetCurrentUserDataAsync();

            await base.OnParametersSetAsync();
        }

        private async Task AddReviewAsync()
        {
            var review = mapper.Map<Review>(reviewRequest);
            var getResponse  = await reviewService.AddReviewAsync(MovieId, currentUser.Value.UserId, review);
            result = mapper.Map<Result<ReviewResponse>>(getResponse);            
        }
    }
}

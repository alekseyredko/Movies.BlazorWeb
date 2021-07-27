using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Movies.Data.Models;
using Movies.Data.Results;
using Movies.Data.Services.Interfaces;
using Movies.Infrastructure.Models.Review;
using Movies.Infrastructure.Models.User;
using Movies.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Movies.BlazorWeb.Pages.Reviews
{
    [Authorize(Roles = "Reviewer")]
    public partial class EditReview
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private IReviewService reviewService { get; set; }

        [Inject]
        private ICustomAuthentication authentication { get; set; }

        [Inject]
        private IMapper mapper { get; set; }

        [Parameter]
        public int Id { get; set; }

        //[CascadingParameter]
        //private Task<AuthenticationState> authenticationStateTask { get; set; }

        private ReviewRequest reviewRequest { get; set; }
        private Result<ReviewResponse> result { get; set; }
        private Result<GetUserResponse> currentUser { get; set; }

        private int userId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            reviewRequest = new ReviewRequest();
            currentUser = await authentication.GetCurrentUserDataAsync();

            var toEdit = await reviewService.GetReviewAsync(Id);
            result = mapper.Map<Result<ReviewResponse>>(toEdit);

            await base.OnParametersSetAsync();
        }

        private async Task EditReviewAsync()
        {
            var review = mapper.Map<Review>(reviewRequest);
            var getResponse = await reviewService.UpdateReviewAsync(Id, currentUser.Value.UserId, review);
            result = mapper.Map<Result<ReviewResponse>>(getResponse);

            if (result.ResultType == ResultType.Ok)
            {
                navigationManager.NavigateTo($"/reviews/{Id}");
            }
        }
    }
}

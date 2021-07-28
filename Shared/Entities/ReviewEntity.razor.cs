using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Movies.Data.Models;
using Movies.Infrastructure.Models.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Movies.BlazorWeb.Shared.Entities
{
    public partial class ReviewEntity
    {
        [Parameter]
        public EventCallback<ReviewResponse> OnDelete { get; set; }

        [Parameter]
        public EventCallback<ReviewResponse> OnEdit { get; set; }

        [Parameter]
        public ReviewResponse Review { get; set; }
       
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        private bool canEditAndDelete { get; set; }     

        private int userId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var state = await authenticationStateTask;

            if (state.User.Identity.IsAuthenticated)
            {
                var claim = state.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (claim != null)
                {
                    userId = int.Parse(claim.Value);

                    if (state.User.IsInRole(Enum.GetName(UserRoles.Producer)) && Review.ReviewerId == userId)
                    {
                        canEditAndDelete = true;
                    }
                }
            }

            //movieLink = $"/movies/{Movie.MovieId}";

            await base.OnParametersSetAsync();
        }

        private async Task OnDeleteAsync(ReviewResponse review)
        {
            await OnDelete.InvokeAsync(review);
        }

        private async Task OnUpdateAsync(ReviewResponse review)
        {
            await OnEdit.InvokeAsync(review);
        }
    }
}

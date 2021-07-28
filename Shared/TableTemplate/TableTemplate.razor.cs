using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Movies.Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.BlazorWeb.Shared.TableTemplate
{
    public partial class TableTemplate<TItem>
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Parameter]
        public RenderFragment TableHeader { get; set; }

        [Parameter]
        public RenderFragment<TItem> RowTemplate { get; set; }
           
        [Parameter]
        public Result<IEnumerable<TItem>> Items { get; set; }
    }
}

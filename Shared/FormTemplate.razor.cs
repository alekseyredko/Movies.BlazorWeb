using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.BlazorWeb.Shared
{
    public partial class FormTemplate<TItem, TResult>
    {
        [Parameter]
        public TItem Item { get; set; }

        [Parameter]
        public TResult Result { get; set; }
    }
}

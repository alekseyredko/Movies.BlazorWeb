using Microsoft.AspNetCore.Components;
using Movies.Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.BlazorWeb.Shared
{
    public partial class ErrorResult
    {
        [Parameter]
        public Result Result { get; set; }
    }
}

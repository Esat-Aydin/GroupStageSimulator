using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Simulator.Services;
using Simulator.Models;

namespace Simulator.Components
{
    public partial class MatchResults
    {
        [Inject] MatchStateService matchState {get; set;}
        [Parameter] public List<Match> Matches { get; set; }


        protected override void OnInitialized(){
            matchState.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            matchState.OnChange -= StateHasChanged;
        }
    }
}
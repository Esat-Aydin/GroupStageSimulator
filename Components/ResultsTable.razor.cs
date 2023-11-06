using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulator.Models;
using Simulator.Interfaces;
using Microsoft.AspNetCore.Components;


namespace Simulator.Components;

    public partial class ResultsTable
    {
        [Inject] Services.TournamentStateService tournamentState {get; set;}

        [Parameter] public List<Team> Teams { get; set; }

        protected override void OnInitialized(){
            tournamentState.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            tournamentState.OnChange -= StateHasChanged;
        }
    }

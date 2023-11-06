using Simulator.Interfaces;
using Microsoft.AspNetCore.Components;
using Simulator.Models;
using System.Globalization;
using System;

namespace Simulator.Components;

public partial class TeamSetup
{
    [Inject] INameValidator<string> teamNameValidator { get; set; }
    [Inject] Services.SimulationService simulationService { get; set; }
    [Inject] Services.TournamentStateService tournamentStateService { get; set; }
    [Inject] Services.MatchStateService matchStateService { get; set; }

    [Parameter] public EventCallback OnSimulationCompleted { get; set; }

    public bool ShowGroupTable { get; set; } = false;
    public bool ShowGroupMatches { get; set; } = false;


    private string[] TeamErrors { get; } = new string[4];

    private char[] AlphabetUpper { get; } = new char[26] {
      'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K',
      'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
      'W', 'X', 'Y', 'Z'
    };

    private List<Team> Teams { get; set; } = new List<Team>
    {
        new Team(),
        new Team(),
        new Team(),
        new Team()
    };

    private bool ShowTeamsInput { get; set; } = false;

    private void TeamsInputChange(){
        ShowTeamsInput = !ShowTeamsInput;
        ShowGroupTable = !ShowGroupTable;
        ShowGroupMatches = !ShowGroupMatches;
        tournamentStateService.SetShowTable(ShowGroupTable);
        matchStateService.SetShowMatches(ShowGroupMatches);
        ClearTeams();
    }
    private void ClearTeams(){
        for (int i = 0; i < Teams.Count; i++)
        {
            Teams[i] = new Team();
        }  
    }
    private async Task ValidateAndSaveTeams()
    {
        bool allValid = true;
        Array.Clear(TeamErrors, 0, TeamErrors.Length);

        for (int i = 0; i < Teams.Count; i++)
        {
            if (!teamNameValidator.IsValid(Teams[i].Name))
            {
                TeamErrors[i] = teamNameValidator.ErrorMessage;
                allValid = false;
            }
        }
        if (allValid)
        {
            await RandomStrengthGenerator();
            simulationService.SaveTeams(Teams);
            TeamsInputChange();
            await OnSimulationCompleted.InvokeAsync();
        }
    }

    private async Task RandomStrengthGenerator()
    {
        Random random = new Random();
        foreach (var team in Teams)
        {
            team.Strength = random.Next(1, 100);
        }
    }

    string BindTeamName(int index)
    {
        return Teams[index].Name;
    }

    void SetTeamName(int index, string value)
    {
        string LowerCaseValue = value.ToLower();
        Teams[index].Name = LowerCaseValue;
    }

    System.Linq.Expressions.Expression<Func<string>> ForTeamName(int index)
    {
        return () => Teams[index].Name;
    }
}
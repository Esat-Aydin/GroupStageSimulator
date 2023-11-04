using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulator.Models;
using Simulator.Components;
using System.Dynamic;

namespace Simulator.Services
{
    public class SimulationService
    {
        private List<Team> Teams = new List<Team>();
        private static readonly Random random = new Random();

        public async void SaveTeams(List<Team> teams)
        {
            Teams.Clear();
            Teams.AddRange(teams);
            await SimulateMatches(Teams);
            await GroupPlaces();
        }

        public IReadOnlyList<Team> GetAllTeams()
        {
            return Teams.AsReadOnly();
        }

        public async Task GroupPlaces(){
            Teams = Teams.OrderByDescending(t => t.Points).ThenByDescending(t => t.GoalDifference).ThenByDescending(t => t.GoalsScored).ToList();
        }

        public async Task SimulateMatches(List<Team> teams)
        {
            List<Team> randomizedTeams = teams.OrderBy(t => Guid.NewGuid()).ToList();

            for (int i = 0; i < randomizedTeams.Count; i += 2)
            {
                var team1 = randomizedTeams[i];
                var team2 = randomizedTeams[i + 1];

                Match match = new Match
                {
                    HomeTeam = team1,
                    AwayTeam = team2
                };
                SimulateScore(match);
            }
        }

        public void SimulateScore(Match match)
        {
            var homeTeam = match.HomeTeam;
            var awayTeam = match.AwayTeam;

            if (homeTeam == null || awayTeam == null)
            {
                throw new Exception("Team not found");
            }

            var homeTeamStrength = homeTeam.Strength;
            var awayTeamStrength = awayTeam.Strength;

            var homeTeamGoals = 0;
            var awayTeamGoals = 0;

            //If you call SimulateMatches or SimulateScore in quick succession,
            //you might get the same random values, 
            //since Random is based on system time. To fix this, you can create a private static instance of Random for the class.

            var homeTeamScoreChance = random.Next(0, homeTeamStrength);
            var awayTeamScoreChance = random.Next(0, awayTeamStrength);

            if (homeTeamScoreChance > awayTeamScoreChance)
            {
                homeTeamGoals = random.Next(1, 5);
                awayTeamGoals = random.Next(0, homeTeamGoals);
            }
            else if (homeTeamScoreChance < awayTeamScoreChance)
            {
                awayTeamGoals = random.Next(1, 5);
                homeTeamGoals = random.Next(0, awayTeamGoals);
            }
            else
            {
                homeTeamGoals = random.Next(0, 5);
                awayTeamGoals = random.Next(0, 5);
            }

            homeTeam.GoalsScored += homeTeamGoals;
            homeTeam.GoalsConceded += awayTeamGoals;
            awayTeam.GoalsScored += awayTeamGoals;
            awayTeam.GoalsConceded += homeTeamGoals;

            if (homeTeamGoals > awayTeamGoals)
            {
                homeTeam.Points += 3;
            }
            else if (homeTeamGoals < awayTeamGoals)
            {
                awayTeam.Points += 3;
            }
            else
            {
                homeTeam.Points += 1;
                awayTeam.Points += 1;
            }
        }
    }
}
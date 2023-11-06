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
        private List<Match> Matches = new List<Match>();
        private static readonly Random random = new Random();

        public async Task SaveTeams(List<Team> teams)
        {
            Teams.Clear();
            Matches.Clear();
            Teams.AddRange(teams); 
            await SimulateMatches(Teams);
            await GroupPlaces();
        }

        public IReadOnlyList<Team> GetAllTeams()
        {
            return Teams.AsReadOnly();
        }

        public IReadOnlyList<Match> GetMatchResults()
        {
            return Matches.AsReadOnly();
        }

        public async Task GroupPlaces(){
            Teams = Teams.OrderByDescending(t => t.Points).ThenByDescending(t => t.GoalDifference).ThenByDescending(t => t.GoalsScored).ToList();
        }

        public async Task SimulateMatches(List<Team> teams)
        {
            int rounds = 3;
            if (teams.Count % 2 != 0)
            {
                throw new Exception("SimulateMatches requires an even number of teams.");
            }

            int matchesPerRound = teams.Count / 2;

            for (int round = 0; round < rounds; round++)
            {
                Console.WriteLine($"Round {round + 1}");

                for (int match = 0; match < matchesPerRound; match++)
                {
                    var homeTeam = teams[match];
                    var awayTeam = teams[teams.Count - match - 1];

                    if (round % 2 == 1)
                    {
                        SimulateScore(new Match { HomeTeam = awayTeam, AwayTeam = homeTeam });
                    }
                    else
                    {
                        SimulateScore(new Match { HomeTeam = homeTeam, AwayTeam = awayTeam });
                    }
                }

                RotateTeams(teams);
            }
        }

        private void RotateTeams(List<Team> teams)
        {
            if (teams.Count <= 1)
            {
                return;
            }

            Team firstTeam = teams[0];
            Team secondTeam = teams[1];

            // Move the second team to the end and shift the rest up
            teams.RemoveAt(1);
            teams.Add(secondTeam);

            // Keep the first team fixed in its position (typically for round-robin, one team stays in place)
            teams[0] = firstTeam;
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

            match.HomeScore = homeTeamGoals;
            match.AwayScore = awayTeamGoals;

            homeTeam.GoalsScored += homeTeamGoals;
            homeTeam.GoalsConceded += awayTeamGoals;
            awayTeam.GoalsScored += awayTeamGoals;
            awayTeam.GoalsConceded += homeTeamGoals;

            // Determine the outcome of the match and update records
            if (homeTeamGoals > awayTeamGoals)
            {
                homeTeam.Points += 3;
                homeTeam.Wins += 1;     // Increment wins for the home team
                awayTeam.Losses += 1;   // Increment losses for the away team
            }
            else if (homeTeamGoals < awayTeamGoals)
            {
                awayTeam.Points += 3;
                awayTeam.Wins += 1;     // Increment wins for the away team
                homeTeam.Losses += 1;   // Increment losses for the home team
            }
            else // it's a draw
            {
                homeTeam.Points += 1;
                awayTeam.Points += 1;
                homeTeam.Draws += 1;    // Increment draws for the home team
                awayTeam.Draws += 1;    // Increment draws for the away team
            }

            Matches.Add(match);

        }
    }
}
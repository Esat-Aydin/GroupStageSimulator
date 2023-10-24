using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simulator.Models;

namespace Simulator.Services
{
    public class SimulationService
    {
        private Random _rand = new Random();

        public (int, int) SimulateMatch(Team home, Team away)
        {
            int homeGoals = _rand.Next(0, home.Strength / 10);
            int awayGoals = _rand.Next(0, away.Strength / 10);
            
            // Handle points and update team stats...
            // ...

            return (homeGoals, awayGoals);
        }

        // Add more methods if needed, like sorting the table, etc.
    }
}
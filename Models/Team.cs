using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simulator.Models
{
    public class Team
    {
        public string Name { get; set; } = string.Empty;
        public int Strength { get; set; }
        public int Points { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public int GoalDifference => GoalsScored - GoalsConceded;
    }
}
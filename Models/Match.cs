using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simulator.Models
{
    public class Match
    {
        public Team? HomeTeam { get; set; }
        public int HomeScore {get; set;}
        public Team? AwayTeam { get; set; }
        public int  AwayScore {get; set;}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Simulator.Interfaces;

namespace Simulator.Utilities
{
    public class TeamNameValidator : INameValidator<string>
    {
        private const string NamePattern = "^[a-zA-Z]{4,14}$";
        private static readonly Regex NameRegex = new Regex(NamePattern);

        public bool IsValid(string teamName) => teamName != null && NameRegex.IsMatch(teamName);

        public string ErrorMessage => "Team name must be between 4 and 14 characters long and only contain letters.";
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simulator.Services
{
    public class MatchStateService
    {
        public bool ShowMatches { get; set; } = true;

        public event Action? OnChange;

        public void SetShowMatches(bool showMatches)
        {
            ShowMatches = showMatches;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
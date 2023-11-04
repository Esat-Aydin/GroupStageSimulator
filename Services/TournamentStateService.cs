using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simulator.Services
{
    public class TournamentStateService
    {
        public bool ShowTable { get; set; } = true;

        public event Action? OnChange;

        public void SetShowTable(bool showTable)
        {
            ShowTable = showTable;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

}
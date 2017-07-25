using FootballDataOrgApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp2.ViewModels
{
    public class LeagueTableViewModel
    {
        public List<LeagueTableTeam> Teams { get; set; }
    }

    public class LeagueTableTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Position { get; set; }        
        public int Points { get; set; }
        public int GoalDifference { get; set; }
    }

    public class LeagueSearchViewModel
    {
        public LeagueSearchResults SearchResults { get; set; }
        public string CurrentLeagueName { get; set; }
        public bool HasCurrentLeague { get { return CurrentLeagueName != string.Empty; } }
    }

    public static class LeagueSearchDetails
    {
        public static int CurrentLeagueId
        {
            get => App.AppSettings.GetValueOrDefault(nameof(CurrentLeagueId), 0);
            set => App.AppSettings.AddOrUpdateValue(nameof(CurrentLeagueId), value);
        }

        public static string CurrentLeagueName
        {
            get => App.AppSettings.GetValueOrDefault(nameof(CurrentLeagueName), string.Empty);
            set => App.AppSettings.AddOrUpdateValue(nameof(CurrentLeagueName), value);
        }
    }
}

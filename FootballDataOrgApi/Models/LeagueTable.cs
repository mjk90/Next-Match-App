using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballDataOrgApi.Models
{
    public class LeagueSearchResults
    {
        public LeagueSearchResults()
        {
            Leagues = new ObservableCollection<LeagueDetails>();
        }
        public string SearchQuery { get; set; }
        public int Count { get; set; }
        public ObservableCollection<LeagueDetails> Leagues { get; set; }
    }

    public class LeagueDetails
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string League { get; set; }
        public string Year { get; set; }
        public int CurrentMatchday { get; set; }
        public int NumberOfMatchdays { get; set; }
        public int NumberOfTeams { get; set; }
        public int NumberOfGames { get; set; }

        public override string ToString()
        {
            return Caption;
        }
    }

    public class LeagueTable
    {
        public string LeagueCaption { get; set; }
        public int MatchDay { get; set; }
        public List<LeagueStanding> Standing { get; set; }
    }

    public class LeagueStanding
    {
        public int Position { get; set; }
        public string TeamName { get; set; }
        public string CrestUri { get; set; }
        public int PlayedGames { get; set; }
        public int Points { get; set; }
        public int Goals { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public HomeRecord Home { get; set; }
        public AwayRecord Away { get; set; }
    }

    public class HomeRecord
    {
        public int Goals { get; set; }
        public int GoalsAgainst { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
    }

    public class AwayRecord
    {
        public int Goals { get; set; }
        public int GoalsAgainst { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
    }
}

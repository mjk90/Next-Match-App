using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballDataOrgApi.Models
{
    public class FixtureSearchResults
    {
        public string SearchQuery { get; set; }
        public int Count { get; set; }
        public List<Fixture> Fixtures { get; set; }
    }

    public class Fixture
    {
        public FixtureLinks _Links { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int MatchDay { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public FixtureResult Result { get; set; }
        public FixtureOdds Odds { get; set; }

        public string Title { get { return HomeTeamName + " vs " + AwayTeamName; } }
        public string Score { get { return Result.GoalsHomeTeam + " - " + Result.GoalsAwayTeam; } }
        public string MatchDayString { get { return "Matchday " + MatchDay; } }
        public string MatchResult { get; set; }

        public string BackgroundColour { get; set; } 
    }

    public class FixtureOdds
    {
        public float HomeWin { get; set; }
        public float Draw { get; set; }
        public float AwayWin { get; set; }
    }

    public class FixtureResult
    {
        public int? GoalsHomeTeam { get; set; }
        public int? GoalsAwayTeam { get; set; }
    }

    public class FixtureLinks
    {
        public Link Self { get; set; }
        public Link Competition { get; set; }
        public Link HomeTeam { get; set; }
        public Link AwayTeam { get; set; }
    }

    public class Link
    {
        public string Href { get; set; }
    }
}

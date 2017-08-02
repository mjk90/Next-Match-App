using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FootballDataApi.Models
{
    public class TeamSearchResults
    {
        public string SearchQuery { get; set; }
        public int Count { get; set; }
        public List<Team> Teams { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int CurrentCompetition { get; set; }
        public string CurrentLeague { get; set; }
    }
}
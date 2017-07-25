using FootballDataOrgApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp2.ViewModels
{
    public class HomePageViewModel
    {
        public Team Team { get; set; }
        public Fixture NextMatch { get; set; }
        public Team RivalTeam { get; set; }
        public string NextMatchAgainst { get; set; }
    }
}

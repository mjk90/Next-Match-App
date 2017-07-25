using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballDataOrgApi.Models
{
    public class TeamSearchResults
    {
        public TeamSearchResults()
        {
            Teams = new ObservableCollection<Team>();
        }
        public string SearchQuery { get; set; }
        public int Count { get; set; }
        public ObservableCollection<Team> Teams { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CrestUrl { get; set; }
        public int? CurrentCompetition { get; set; }
        public string CurrentLeague { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

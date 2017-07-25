using FootballDataOrgApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FootballDataOrgApi
{
    public class FootballApi
    {
        private const string TeamSearchUrl = "http://api.football-data.org/v1/teams?name={0}";
        private const string TeamUrl = "http://api.football-data.org/v1/teams/{0}";
        private const string TeamFixturesUrl = "http://api.football-data.org/v1/teams/{0}/fixtures";
        private const string LeagueSearchUrl = "http://api.football-data.org/v1/competitions/?season={0}";
        private const string LeagueTableUrl = "http://api.football-data.org/v1/competitions/{0}/leagueTable";

        public TeamSearchResults SearchForTeam(string teamName)
        {
            string results = Get(string.Format(TeamSearchUrl, teamName));

            TeamSearchResults result = JsonConvert.DeserializeObject<TeamSearchResults>(results);
            return result;
        }

        public Team GetTeamDetails(int teamId)
        {
            string results = Get(string.Format(TeamUrl, teamId));

            Team result = JsonConvert.DeserializeObject<Team>(results);
            return result;
        }

        public Team GetTeamDetails(string teamUrl)
        {
            string results = Get(teamUrl);

            Team result = JsonConvert.DeserializeObject<Team>(results);
            return result;
        }

        public Fixture GetNextFixtureForTeam(int teamId)
        {
            string results = Get(string.Format(TeamFixturesUrl, teamId) + "?timeFrame=n99");

            FixtureSearchResults result = JsonConvert.DeserializeObject<FixtureSearchResults>(results);
            return result.Fixtures.FirstOrDefault();
        }

        public FixtureSearchResults GetFixturesForTeam(int teamId)
        {
            string results = Get(string.Format(TeamFixturesUrl, teamId) + "?season=2015");

            FixtureSearchResults result = JsonConvert.DeserializeObject<FixtureSearchResults>(results);
            return result;
        }

        public LeagueSearchResults SearchForLeague(string year)
        {
            string results = Get(string.Format(LeagueSearchUrl, year));

            LeagueSearchResults result = new LeagueSearchResults
            {
                Leagues = JsonConvert.DeserializeObject<ObservableCollection<LeagueDetails>>(results)
            };
            return result;
        }

        public LeagueTable GetLeagueTable(int leagueId)
        {
            string results = Get(string.Format(LeagueTableUrl, leagueId));

            LeagueTable result = JsonConvert.DeserializeObject<LeagueTable>(results);
            return result;
        }

        #region Generic Helpers
        private string Get(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Auth-Token", "58ccae0ceea64bc38c51c3d7fc6ed05d");

            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
        }

        private void Post(string url, string jsonContent)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            long length = 0;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    length = response.ContentLength;
                }
            }
            catch (WebException ex)
            {
                // Log exception and throw as for GET example above
            }
        }
        #endregion
    }
}

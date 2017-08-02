using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using FootballDataOrgApi;
using FootballDataOrgApi.Models;

namespace Match
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a team name:");
            string teamName = Console.ReadLine();

            FootballApi api = new FootballApi();
            TeamSearchResults teamSearchSesults = api.SearchForTeam(teamName);

            if(teamSearchSesults != default(TeamSearchResults))
            {
                Console.WriteLine("Please select your team: ");
                int teamCount = 1;
                foreach(Team team in teamSearchSesults.Teams)
                {
                    Console.WriteLine( string.Format("[{0}] - {1}", teamCount, team.Name) );
                    teamCount++;
                }

                int teamChoice = Convert.ToInt32(Console.ReadLine()) - 1;

                if(teamChoice < teamCount - 1)
                {
                    Team chosenTeam = teamSearchSesults.Teams[teamChoice];
                    FixtureSearchResults fixtureSearchResults = api.GetFixturesForTeam(chosenTeam.Id, DateTime.UtcNow.Year);

                    if (fixtureSearchResults != default(FixtureSearchResults))
                    {
                        foreach (Fixture fixture in fixtureSearchResults.Fixtures)
                        {
                            Console.WriteLine(fixture.HomeTeamName + "(" + fixture.Result.GoalsHomeTeam + ")" + " vs " + fixture.AwayTeamName + "(" + fixture.Result.GoalsAwayTeam + ")");
                        }
                    }
                    Console.ReadLine();
                }
            }
        }        
    }
}

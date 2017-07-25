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
using FootballDataOrgApi;
using FootballDataOrgApi.Models;

namespace MatchApp
{
    [Activity(Label = "TeamFixtures")]
    public class TeamFixtures : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            var teamFixtures = Intent.Extras.GetStringArrayList("teamFixtures") ?? new string[0];
            int teamId = Intent.GetIntExtra("teamId", -1);

            if (teamId > -1)
            {
                FootballApi api = new FootballApi();
                FixtureSearchResults fixtureSearchResults = api.GetFixturesForTeam(teamId);

                if (fixtureSearchResults != default(FixtureSearchResults))
                {
                    this.ListAdapter = new ArrayAdapter<Fixture>(this, Android.Resource.Layout.SimpleListItem1, fixtureSearchResults.Fixtures);
                }
            }
        }
    }
}
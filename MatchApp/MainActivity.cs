using Android.App;
using Android.Widget;
using Android.OS;
using System;
using FootballDataOrgApi;
using FootballDataOrgApi.Models;
using System.Collections.Generic;
using Android.Views;
using Android.Content;
using Xamarin.Forms;

namespace MatchApp
{
    [Activity(Label = "MatchApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Android.Widget.ListView teamSearchList;
        public List<string> teamFixtures = new List<string>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            EditText teamNameText = FindViewById<EditText>(Resource.Id.teamNameText);
            Android.Widget.Button searchButton = FindViewById<Android.Widget.Button>(Resource.Id.teamSearchButton);
            TextView resultsText = FindViewById<TextView>(Resource.Id.teamSearchResultsText);
            teamSearchList = FindViewById<Android.Widget.ListView>(Resource.Id.teamSearchListView);

            string searchResultsText = string.Empty;

            searchButton.Click += (object sender, EventArgs e) =>
            {
                string teamName = teamNameText.Text;

                if (teamName != string.Empty)
                {
                    FootballApi api = new FootballApi();
                    TeamSearchResults teamSearchSesults = api.SearchForTeam(teamName);

                    if (teamSearchSesults != default(TeamSearchResults))
                    {
                        teamSearchList.Adapter = new ArrayAdapter<Team>(this, Android.Resource.Layout.SimpleListItem1, teamSearchSesults.Teams);
                        teamSearchList.ItemClick += TeamListClick;
                    }

                    resultsText.Text = searchResultsText;
                }
            };
        }

        private void TeamListClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Get our item from the list adapter
            var item = this.teamSearchList.GetItemAtPosition(e.Position);
            Team selectedTeam = item.Cast<Team>();

            //Make a toast with the item name just to show it was clicked
            //Toast.MakeText(this, selectedTeam.Name + " Clicked! Id: " + selectedTeam.Id, ToastLength.Short).Show();

            //var intent = new Intent(this, typeof(CarauselPage));
            //intent.PutExtra("teamId", selectedTeam.Id);
            //StartActivity(intent);
            //var np = new NavigationPage(new TeamFixturesCarausel());
            // MainPage = np;

            var page = new CarauselPage();
            page.Title = "Test";
            //Navigation
        }
    }

    public static class ObjectTypeHelper
    {
        public static T Cast<T>(this Java.Lang.Object obj) where T : class
        {
            var propertyInfo = obj.GetType().GetProperty("Instance");
            return propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as T;
        }
    }
}


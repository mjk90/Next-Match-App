using FootballDataOrgApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace TestApp2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        public HomePageViewModel ViewModel { get; set; }
        public FootballApi Api { get; set; }

        public HomePage ()
        {
            Api = new FootballApi();
            InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var selectTeamMessage = this.FindByName<StackLayout>("NoTeamSelected");
            var pageDetails = this.FindByName<StackLayout>("HomePageDetails");

            if (SelectTeamDetails.CurrentTeamId == 0)
            {
                selectTeamMessage.IsVisible = true;
                pageDetails.IsVisible = false;
            }
            else
            {
                var nextMatch = Api.GetNextFixtureForTeam(SelectTeamDetails.CurrentTeamId);
                bool isHomeTeam = nextMatch.HomeTeamName.ToLower() == SelectTeamDetails.CurrentTeamName.ToLower();
                var team = Api.GetTeamDetails(SelectTeamDetails.CurrentTeamId);
                var rivalTeam = Api.GetTeamDetails(isHomeTeam ? nextMatch._Links.AwayTeam.Href : nextMatch._Links.HomeTeam.Href);

                ViewModel = new HomePageViewModel
                {
                    //todo: get team from API
                    //Team = new FootballDataOrgApi.Models.Team
                    //{
                    //    Id = SelectTeamDetails.CurrentTeamId,
                    //    Name = SelectTeamDetails.CurrentTeamName,
                    //    CurrentCompetition = 1,
                    //    CurrentLeague = "Premier League"
                    //},
                    Team = team,
                    NextMatch = nextMatch,
                    RivalTeam = rivalTeam
                };

                ViewModel.NextMatchAgainst = ViewModel.NextMatch.HomeTeamName == ViewModel.Team.Name ? ViewModel.NextMatch.AwayTeamName : ViewModel.NextMatch.HomeTeamName;
                BindingContext = ViewModel;
                selectTeamMessage.IsVisible = false;
                pageDetails.IsVisible = true;
            }
        }

        private void SelectLeagueClicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new SelectLeaguePage());
        }
        private void SelectTeamClicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new SelectTeamPage());
        }
        private void SettingsClicked(object sender, EventArgs e)
        {
            //this.Navigation.PushAsync(new SelectLeaguePage());
        }
    }
}
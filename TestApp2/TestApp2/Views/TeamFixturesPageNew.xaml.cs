using FootballDataOrgApi;
using FootballDataOrgApi.Models;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp2.Models;
using TestApp2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TeamFixturesPageNew : ContentPage
    {
        public FootballApi Api { get; set; }
        public FixtureSearchResults SearchResults { get; set; }
        private DotButtonsLayout dotLayout;

        public TeamFixturesPageNew()
        {
            SearchResults = new FixtureSearchResults { Fixtures = new List<Fixture> { new Fixture { BackgroundColour = "#ffffff", MatchDay = 1, HomeTeamName = "Team 1", AwayTeamName = "Team 2", Result = new FixtureResult { GoalsAwayTeam = 0, GoalsHomeTeam = 0 }, MatchResult = "Draw" } }, Count = 0 };
            InitializeComponent();

            Api = new FootballApi();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SearchResults.Fixtures.Clear();
            var selectTeamMessage = this.FindByName<StackLayout>("NoTeamSelected");

            if (SelectTeamDetails.CurrentTeamId == 0)
            {
                selectTeamMessage.IsVisible = true;
            }
            else
            {
                FixtureSearchResults fixtureSearchResults = Api.GetFixturesForTeam(SelectTeamDetails.CurrentTeamId);

                var total = fixtureSearchResults.Fixtures.Count;
                var pageSize = 25; // set your page size, which is number of records per page
                var page = this.FindByName<CarouselView>("FixturesCarousel").Position < 1 ? 1 : this.FindByName<CarouselView>("FixturesCarousel").Position; // set current page number, must be >= 1
                var skip = page - (pageSize / 2) > 0 ? page - (pageSize / 2) : 0;

                fixtureSearchResults.Fixtures = fixtureSearchResults.Fixtures.Skip(skip).Take(pageSize).ToList();
                fixtureSearchResults.SearchQuery = SelectTeamDetails.CurrentTeamName;

                Title = "Fixtures for " + SelectTeamDetails.CurrentTeamName;

                foreach (Fixture fixture in fixtureSearchResults.Fixtures)
                {
                    bool isHomeTeam = fixture.HomeTeamName.ToLower() == fixtureSearchResults.SearchQuery.ToLower();
                    int result = isHomeTeam ? (fixture.Result.GoalsHomeTeam > fixture.Result.GoalsAwayTeam ? 1 :
                                                fixture.Result.GoalsHomeTeam < fixture.Result.GoalsAwayTeam ? -1 :
                                                0) :
                                                (fixture.Result.GoalsHomeTeam < fixture.Result.GoalsAwayTeam ? 1 :
                                                fixture.Result.GoalsHomeTeam > fixture.Result.GoalsAwayTeam ? -1 :
                                                0);
                    fixture.BackgroundColour = result == 1 ? "#9ccc64" :
                                                result == -1 ? "#f44335" :
                                                "#f9bf2d";
                    fixture.MatchResult = result == 1 ? "Win" : result == -1 ? "Loss" : "Draw";
                    SearchResults.Fixtures.Add(fixture);
                }

                dotLayout = new DotButtonsLayout(fixtureSearchResults.Fixtures.Count, Color.Black, 7);
                foreach (DotButton dot in dotLayout.dots)
                    dot.Clicked += DotClicked;
                this.FindByName<StackLayout>("DotsContainer").Children.Clear();
                this.FindByName<StackLayout>("DotsContainer").Children.Add(dotLayout);

                // todo: don't trust this vv. It's not working properly
                this.FindByName<CarouselView>("FixturesCarousel").Position = this.FindByName<CarouselView>("FixturesCarousel").Position - skip;
                SetDotPosition(this.FindByName<CarouselView>("FixturesCarousel").Position);

                BindingContext = fixtureSearchResults;
                selectTeamMessage.IsVisible = false;
            }
        }

        //The function called by the buttons clicked event
        private void DotClicked(object sender)
        {
            var button = (DotButton)sender;
            //Get the selected buttons index
            int index = button.index;
            //Set the corresponding page as position of the carousel view
            this.FindByName<CarouselView>("FixturesCarousel").Position = index;
        }

        //The function that is called when the user swipes trough pages
        private void PageChanged(object sender, SelectedPositionChangedEventArgs e)
        {
            //Get the selected page
            SetDotPosition((int)(e.SelectedPosition));
        }

        private void SetDotPosition(int position)
        {
            for (int i = 0; i < dotLayout.dots.Length; i++)
                if (position == i)
                    dotLayout.dots[i].Opacity = 1;
                else
                    dotLayout.dots[i].Opacity = 0.5;
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
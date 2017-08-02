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
        // http://hot-totem.com/blog/post/carouselview-pageindicators-xamarinforms

        public FootballApi Api { get; set; }
        public FixtureSearchResults SearchResults { get; set; }
        private DotButtonsLayout DotLayout;
        private ScrollView DotScrollView;

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
                try
                {
                    FixtureSearchResults fixtureSearchResults = Api.GetFixturesForTeam(SelectTeamDetails.CurrentTeamId, DateTime.UtcNow.Year);
                    fixtureSearchResults.League = Api.GetLeagueDetails(fixtureSearchResults.Fixtures.FirstOrDefault()._Links.Competition.Href);
                    fixtureSearchResults.SearchQuery = SelectTeamDetails.CurrentTeamName;
                    Title = "Fixtures for " + SelectTeamDetails.CurrentTeamName;

                    DateTime tempDate = new DateTime(2017, 8, 26);
                    DateTime closestDate = DateTime.MaxValue;
                    int closestDateIndex = 0;
                    int fixturesCount = 0;
                
                    foreach (Fixture fixture in fixtureSearchResults.Fixtures)
                    {
                        if (fixture.AlreadyPlayed)
                        {
                            bool isHomeTeam = fixture.HomeTeamName.ToLower() ==
                                                fixtureSearchResults.SearchQuery.ToLower();
                            int result = isHomeTeam
                                ? (fixture.Result.GoalsHomeTeam > fixture.Result.GoalsAwayTeam
                                    ? 1
                                    : fixture.Result.GoalsHomeTeam < fixture.Result.GoalsAwayTeam
                                        ? -1
                                        : 0)
                                : (fixture.Result.GoalsHomeTeam < fixture.Result.GoalsAwayTeam
                                    ? 1
                                    : fixture.Result.GoalsHomeTeam > fixture.Result.GoalsAwayTeam
                                        ? -1
                                        : 0);
                            fixture.BackgroundColour = result == 1 ? "#9ccc64" : result == -1 ? "#f44335" : "#f9bf2d";
                            fixture.MatchResult = result == 1 ? "Win" : result == -1 ? "Loss" : "Draw";
                        }
                        else
                        {
                            fixture.BackgroundColour = "#DCDCDC";
                            fixture.MatchResult = fixture.Date.ToShortDateString();
                        }

                        if ((closestDate - tempDate).Days > (fixture.Date - tempDate).Days)
                        {
                            closestDate = fixture.Date;
                            closestDateIndex = fixturesCount;
                        }
                        fixturesCount++;
                        SearchResults.Fixtures.Add(fixture);
                    }


                    DotLayout = new DotButtonsLayout(fixtureSearchResults.Fixtures.Count, Color.Black, 12);
                    foreach (DotButton dot in DotLayout.dots)
                        dot.Clicked += async (sender) => await DotClicked(sender);

                    this.FindByName<StackLayout>("DotsContainer").Children.Clear();
                    DotScrollView = new ScrollView();
                    DotScrollView.Orientation = ScrollOrientation.Horizontal;
                    DotScrollView.HeightRequest = 50;
                    DotScrollView.Content = DotLayout;
                    this.FindByName<StackLayout>("DotsContainer").Children.Add(DotScrollView);

                    this.FindByName<CarouselView>("FixturesCarousel").Position = closestDateIndex;

                    SetDotPosition(this.FindByName<CarouselView>("FixturesCarousel").Position);
                    selectTeamMessage.IsVisible = false;

                    BindingContext = fixtureSearchResults;

                }
                catch (Exception e)
                {
                    selectTeamMessage.IsVisible = true;
                }
            }
        }

        //The function called by the buttons clicked event
        private async Task DotClicked(object sender)
        {
            var button = (DotButton)sender;
            //Get the selected buttons index
            int index = button.index;
            //Set the corresponding page as position of the carousel view
            this.FindByName<CarouselView>("FixturesCarousel").Position = index;
            await ScrollDots(button);
        }

        private async Task ScrollDots(Element button)
        {
            await Task.Delay(1);
            await DotScrollView.ScrollToAsync(button, ScrollToPosition.Center, true);
        }

        //The function that is called when the user swipes trough pages
        private async void PageChanged(object sender, SelectedPositionChangedEventArgs e)
        {
            //Get the selected page
            SetDotPosition((int)(e.SelectedPosition));
        }

        private async void SetDotPosition(int position)
        {
            for (int i = 0; i < DotLayout.dots.Length; i++)
                if (position == i)
                {
                    DotLayout.dots[i].Opacity = 1;
                    await ScrollDots(DotLayout.dots[i]);
                }
                else
                    DotLayout.dots[i].Opacity = 0.5;
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
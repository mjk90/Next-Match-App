using FootballDataOrgApi;
using FootballDataOrgApi.Models;
using TestApp2.ViewModels;
using TestApp2.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Plugin.Settings;
using Plugin.Settings.Abstractions;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TestApp2
{    
	public partial class App : Application
    {
        public static ISettings AppSettings => CrossSettings.Current;
        //public static SelectTeamDetails CurrentTeam { get; set; }
        //public static LeagueSearchDetails CurrentLeague { get; set; }

        public App()
		{
			InitializeComponent();

			SetMainPage();
		}

		public static void SetMainPage()
        {
            FootballApi Api = new FootballApi();

            //SelectTeamDetails.CurrentTeamId = 0;
            //SelectTeamDetails.CurrentTeamName = "";
            //LeagueSearchDetails.CurrentLeagueId = 0;
            //LeagueSearchDetails.CurrentLeagueId = 398;
            //LeagueSearchDetails.CurrentLeagueName = "";

            var tabs = new TabbedPage
            {
                Children =
                {
                    //new NavigationPage(new ItemsPage())
                    //{
                    //    Title = "Browse",
                    //    Icon = Device.OnPlatform<string>("tab_feed.png",null,null)
                    //},
                    //new NavigationPage(new AboutPage())
                    //{
                    //    Title = "About",
                    //    Icon = Device.OnPlatform<string>("tab_about.png",null,null)
                    //},
                    new NavigationPage(new HomePage())
                    {
                        Title = "Next Match"
                    },
                    new NavigationPage(new TeamFixturesPageNew())
                    {
                        Title = "Fixtures"
                    },
                    new NavigationPage(new LeagueTablePage())
                    {
                        Title = "League Table"
                    }
                }
            };
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetIsSwipePagingEnabled(tabs, false);
            Current.MainPage = tabs;
        }
	}
}

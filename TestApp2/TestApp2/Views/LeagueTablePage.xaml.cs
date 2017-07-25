using FootballDataOrgApi;
using FootballDataOrgApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp2.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp2.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LeagueTablePage : ContentPage
    {
        public FootballApi Api { get; set; }
        public LeagueTable ViewModel { get; set; }

		public LeagueTablePage ()
		{
			InitializeComponent ();

            Api = new FootballApi();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var selectLeagueMessage = this.FindByName<StackLayout>("NoLeagueSelected");
            var leagueTable = this.FindByName<StackLayout>("LeagueTable");

            if (LeagueSearchDetails.CurrentLeagueId == 0)
            {
                selectLeagueMessage.IsVisible = true;
                leagueTable.IsVisible = false;
            }
            else
            {
                ViewModel = Api.GetLeagueTable(LeagueSearchDetails.CurrentLeagueId);
                Title = ViewModel.LeagueCaption;
                BindingContext = ViewModel;

                selectLeagueMessage.IsVisible = false;
                leagueTable.IsVisible = true;
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
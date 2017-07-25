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
	public partial class SelectLeaguePage : ContentPage
    {
        public FootballApi Api { get; set; }
        public LeagueSearchViewModel ViewModel { get; set; }

        public SelectLeaguePage ()
        {
            Api = new FootballApi();
            ViewModel = new LeagueSearchViewModel
            {
                SearchResults = new LeagueSearchResults(),
                CurrentLeagueName = LeagueSearchDetails.CurrentLeagueName
            };

            InitializeComponent();
            BindingContext = ViewModel;
        }

        private void YearPickerChanged(object sender, EventArgs e)
        {
            if (YearPicker != null && YearPicker.SelectedIndex <= YearPicker.Items.Count)
            {
                var selecteditem = YearPicker.Items[YearPicker.SelectedIndex];

                LeagueSearchResults results = Api.SearchForLeague(selecteditem);
                ViewModel.SearchResults.Leagues.Clear();
                foreach (LeagueDetails league in results.Leagues)
                    ViewModel.SearchResults.Leagues.Add(league);
            }
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var league = args.SelectedItem as LeagueDetails;

            LeagueSearchDetails.CurrentLeagueId = league.Id;
            LeagueSearchDetails.CurrentLeagueName = league.Caption;

            ViewModel.SearchResults.Leagues.Clear();
            ViewModel.SearchResults.SearchQuery = string.Empty;

            await this.Navigation.PopAsync();
        }
    }
}
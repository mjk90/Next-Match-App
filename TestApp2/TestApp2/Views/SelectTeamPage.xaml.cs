using FootballDataOrgApi;
using FootballDataOrgApi.Models;
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
    public partial class SelectTeamPage : ContentPage
    {
        public SelectTeamViewModel ViewModel { get; set; }
        public FootballApi Api { get; set; }

        public SelectTeamPage()
        {
            InitializeComponent();

            ViewModel = new SelectTeamViewModel
            {
                SearchResults = new TeamSearchResults(),
                CurrentTeamName = SelectTeamDetails.CurrentTeamName
            };
            Api = new FootballApi();

            BindingContext = ViewModel;
        }

        void Entry_Completed(object sender, EventArgs e)
        {
            if (ViewModel.SearchResults.SearchQuery != string.Empty)
            {
                TeamSearchResults results = Api.SearchForTeam(ViewModel.SearchResults.SearchQuery);
                ViewModel.SearchResults.Teams.Clear();
                foreach(Team team in results.Teams)
                    ViewModel.SearchResults.Teams.Add(team);
            }
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var team = args.SelectedItem as Team;

            SelectTeamDetails.CurrentTeamId = team.Id;
            SelectTeamDetails.CurrentTeamName = team.Name;

            ViewModel.SearchResults.Teams.Clear();
            ViewModel.SearchResults.SearchQuery = string.Empty;

            await this.Navigation.PopAsync();
        }
    }
}
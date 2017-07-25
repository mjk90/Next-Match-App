using FootballDataOrgApi.Models;
using TestApp2.Helpers;

using Xamarin.Forms;

using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace TestApp2.ViewModels
{
    public class SelectTeamViewModel
    {
        public TeamSearchResults SearchResults { get; set; }
        public string CurrentTeamName { get; set; }
        public bool HasCurrentTeam { get { return CurrentTeamName != string.Empty; } }
    }
    
    public static class SelectTeamDetails
    {
        public static int CurrentTeamId
        {
            get => App.AppSettings.GetValueOrDefault(nameof(CurrentTeamId), 0);
            set => App.AppSettings.AddOrUpdateValue(nameof(CurrentTeamId), value);
        }

        public static string CurrentTeamName
        {
            get => App.AppSettings.GetValueOrDefault(nameof(CurrentTeamName), string.Empty);
            set => App.AppSettings.AddOrUpdateValue(nameof(CurrentTeamName), value);
        }
    }
}

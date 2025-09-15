using BudgetTracker.DataBase;
using BudgetTracker.Model;
using BudgetTracker.Service;
using BudgetTracker.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace BudgetTracker.ViewModel
{
    public partial class StartPageViewModel : INotifyPropertyChanged
    {


        // If there is no User or there is new user made
        public UserService UserService = new();

        private string _username = string.Empty;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                // nameof(_username)
                OnPropertyChanged();
            }
        }








        // Go To Track
        public ICommand GoToNextPageCommand { get; }

        public event Action? RequestNavigationToTrack;
        //private void OnTrack()
        //{
        //    RequestNavigationToTrack?.Invoke();
        //}

        // Go To Analyze
        //public ICommand GoToAnalyzeCommand { get; }

        public event Action? RequestNavigationToAnalyze;

        //private void OnAnalyze()
        //{
        //    RequestNavigationToAnalyze?.Invoke();
        //}

        public StartPageViewModel()
        {
            //GoToTrackCommand = new RelayCommand(OnTrack);
            //GoToAnalyzeCommand = new RelayCommand(OnAnalyze);
            GoToNextPageCommand = new AsyncRelayCommand<string>(ExecuteLoginAsync);

        }



        public event Action? RequestNavigationToNewUser;
        private async Task ExecuteLoginAsync(string? targetPage)
        {
            var name = Username?.Trim();
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return;
            }



            var user = await UserService.GetByNameAsync(name);
            if (user != null)
            {
                if (targetPage == "TrackPage")
                {
                    RequestNavigationToTrack?.Invoke();
                }

                else if (targetPage == "AnalyzePage")
                {
                    RequestNavigationToAnalyze?.Invoke();
                }
            }
            else
            {
                RequestNavigationToNewUser?.Invoke();
            }

        }



    


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

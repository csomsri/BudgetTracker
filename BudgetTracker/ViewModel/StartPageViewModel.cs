using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BudgetTracker.View;
using BudgetTracker.DataBase;
using BudgetTracker.Service;
using Microsoft.UI.Xaml.Navigation;


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



        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            GoToNextPageCommand = new RelayCommand<string>(ExecuteLogin);
            
        }

  

        public event Action? RequestNavigationToNewUser;
        private void ExecuteLogin(string? targetPage)
        {

            if (string.IsNullOrEmpty(Username) || string.IsNullOrWhiteSpace(targetPage)) 
            {
                return;
            }

           

            var user = UserService.GetUserByName(Username);
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



    }
}

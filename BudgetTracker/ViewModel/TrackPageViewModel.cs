using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class TrackPageViewModel : ObservableObject
    {
        //public event PropertyChangedEventHandler? PropertyChanged;

        //protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        // Go Back To StartPage
        // public ICommand GoBackCommand { get; }
        public event Action? RequestGoBack;

        //public TrackPageViewModel()
        //{
        //    GoBackCommand = new RelayCommand(GoBack);

        //}

        [RelayCommand]
        public void GoBack()
        {
            RequestGoBack?.Invoke();
        }

        // Submit Logic
    }
}

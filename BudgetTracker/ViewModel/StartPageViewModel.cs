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

namespace BudgetTracker.ViewModel
{
    public partial class StartPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand GoToTrackCommand { get; }

        public event Action? RequestNavigationToTrack;

        public StartPageViewModel()
        {
            GoToTrackCommand = new RelayCommand(OnTrack);
        }

        private void OnTrack()
        {
            RequestNavigationToTrack?.Invoke();
        }
    }
}

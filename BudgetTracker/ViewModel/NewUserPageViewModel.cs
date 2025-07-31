using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Windows.Networking.NetworkOperators;

namespace BudgetTracker.ViewModel
{
    public partial class NewUserPageViewModel : ObservableObject
    {

        public event Action? RequestGoBack;

        [RelayCommand]
        public void GoBack()
        {
            RequestGoBack?.Invoke();
        }






        // Create Account System
    }
}

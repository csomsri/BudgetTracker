using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.ViewModel
{
    public partial class AnalyzePageViewModel : ObservableObject
    {
        // Go Back Frame
        public event Action? RequestGoBack;

        [RelayCommand]
        public void GoBack()
        {
            RequestGoBack?.Invoke();
        }
    }
}

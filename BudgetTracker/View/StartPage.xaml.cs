using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using BudgetTracker.ViewModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BudgetTracker.View;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class StartPage : Page
{

    StartPageViewModel ViewModel { get; } = new StartPageViewModel();
    public StartPage()
    {
        InitializeComponent();
        mainGrid.DataContext = ViewModel;

        ViewModel.RequestNavigationToTrack += () =>
        {
            Frame.Navigate(typeof(TrackPage));
        };

        ViewModel.RequestNavigationToAnalyze += () =>
        {
            Frame.Navigate(typeof(AnalyzePage));
        };

        ViewModel.RequestNavigationToNewUser += () =>
        {
            try
            {
                Frame.Navigate(typeof(NewUserPage));

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error navigating to NewUserPage: {ex}");
                System.Diagnostics.Debug.WriteLine("Unable to go to NewUserPage");
            }
        };
    }
}

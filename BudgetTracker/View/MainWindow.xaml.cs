using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;
using Microsoft.UI.Windowing;
using System.ComponentModel;
using BudgetTracker.ViewModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BudgetTracker
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {

        public MainWindowViewModel ViewModel { get; } = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            mainGrid.DataContext = new MainWindow();
            
            
            
            
            
            
            
            
            // Window Size
            IntPtr hwnd = WindowNative.GetWindowHandle(this);
            WindowId windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
            AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

            // Set default window size
            appWindow.Resize(new Windows.Graphics.SizeInt32(1000, 1000));

            // Optional: Prevent resizing larger than 1000x1000
            appWindow.Changed += (s, e) =>
            {
                var size = appWindow.Size;
                if (size.Width > 1000 || size.Height > 1000)
                {
                    appWindow.Resize(new Windows.Graphics.SizeInt32(
                        Math.Min(size.Width, 1000),
                        Math.Min(size.Height, 1000)
                    ));
                }
            };
        }
    }
}

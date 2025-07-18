using System;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BudgetTracker.ViewModel
{


	public partial class MainWindowViewModel : INotifyPropertyChanged
	{

		private string username = string.Empty;


		public string Username
		{
			get => username;
			set
			{
				if (username != value)
				{
					username = value;
					OnPropertyChanged();
				}
			}
		}


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propName = null!) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

    }
}

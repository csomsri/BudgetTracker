using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Converters
{
    public sealed class DecimalToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
            => value is decimal d ? (double)d : 0d;

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => value is double x ? (decimal)x : 0m;
    }
}

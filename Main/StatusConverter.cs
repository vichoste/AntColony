using System;
using System.Globalization;
using System.Windows.Data;

namespace AntColony.Main;
internal class StatusConverter : IValueConverter {
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is Status status ? status.ToString() : Status.Ready.ToString();
	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value is string status ? Enum.Parse(typeof(Status), status) : Status.Ready;
}
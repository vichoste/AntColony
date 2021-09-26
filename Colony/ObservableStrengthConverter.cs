using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace AntColony.Colony;
internal class ObservableStrengthConverter : IValueConverter {
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
		if (value is byte observableStrength) {
			var rgb = Color.FromArgb(observableStrength, observableStrength, observableStrength);
			return $"#{rgb.R:X2}{ rgb.G:X2}{rgb.B:X2}";
		}
		return "#FFFFFF";
	}
	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 0;
}
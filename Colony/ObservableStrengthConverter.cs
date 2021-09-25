using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace AntColony.Colony;
internal class ObservableStrengthConverter : IValueConverter {
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
		if (value is byte observableStrength) {
			var r = int.Parse(observableStrength.ToString().Substring(0, 2), NumberStyles.AllowHexSpecifier);
			var g = int.Parse(observableStrength.ToString().Substring(2, 2), NumberStyles.AllowHexSpecifier);
			var b = int.Parse(observableStrength.ToString().Substring(4, 2), NumberStyles.AllowHexSpecifier);
			return Color.FromArgb(r, g, b);
		}
		return Color.FromArgb(0, 0, 0);
	}
	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => 0; // I am both lazy and without much time to implement this lmao
}
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace AntColony.Colony;
/// <summary>
/// Convert observable strength to RGB color
/// Based on: https://stackoverflow.com/questions/13354892/converting-from-rgb-ints-to-observableStrength
/// </summary>
internal class ObservableStrengthConverter : IValueConverter {
	#region Converter methods
	/// <summary>
	/// Convert observable strength to RGB
	/// </summary>
	/// <param name="value"></param>
	/// <param name="targetType"></param>
	/// <param name="parameter"></param>
	/// <param name="culture"></param>
	/// <returns>HEX color</returns>
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
		if (value is byte observableStrength) {
			var r = int.Parse(observableStrength.ToString().Substring(0, 2), NumberStyles.AllowHexSpecifier);
			var g = int.Parse(observableStrength.ToString().Substring(2, 2), NumberStyles.AllowHexSpecifier);
			var b = int.Parse(observableStrength.ToString().Substring(4, 2), NumberStyles.AllowHexSpecifier);
			return Color.FromArgb(r, g, b);
		}
		return Color.FromArgb(0, 0, 0);
	}
	/// <summary>
	/// Convert HEX to RGB
	/// </summary>
	/// <param name="value"></param>
	/// <param name="targetType"></param>
	/// <param name="parameter"></param>
	/// <param name="culture"></param>
	/// <returns>RGB color</returns>
	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
		// I am both lazy and without much time to implement this lmao
		0;
	#endregion
}
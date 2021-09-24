using System;
using System.Globalization;
using System.Windows.Data;

namespace AntColony.Main;
/// <summary>
/// Makes ant count rate observable
/// </summary>
public class AntCountConverter : IValueConverter {
	#region Converter methods
	/// <summary>
	/// Convert ant count to string
	/// </summary>
	/// <param name="value"></param>
	/// <param name="targetType"></param>
	/// <param name="parameter"></param>
	/// <param name="culture"></param>
	/// <returns>Status as string</returns>
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is int antCount ? antCount.ToString() : MainModel.MaxAntCount.ToString();

	/// <summary>
	/// Convert ant count to int
	/// </summary>
	/// <param name="value"></param>
	/// <param name="targetType"></param>
	/// <param name="parameter"></param>
	/// <param name="culture"></param>
	/// <returns>Status as enum</returns>
	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value is string antCount && int.TryParse(antCount, out var result) ? result : MainModel.MinAntCount;
	#endregion
}
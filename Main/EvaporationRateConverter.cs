using System;
using System.Globalization;
using System.Windows.Data;

namespace AntColony.Main;
/// <summary>
/// Makes evaporation rate observable
/// </summary>
internal class EvaporationRateConverter : IValueConverter {
	#region Converter methods
	/// <summary>
	/// Convert ant count to string
	/// </summary>
	/// <param name="value"></param>
	/// <param name="targetType"></param>
	/// <param name="parameter"></param>
	/// <param name="culture"></param>
	/// <returns>Status as string</returns>
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
		return value is double antCount ? antCount.ToString() : MainModel.MinEvaporationRate.ToString();
	}

	/// <summary>
	/// Convert ant count to double
	/// </summary>
	/// <param name="value"></param>
	/// <param name="targetType"></param>
	/// <param name="parameter"></param>
	/// <param name="culture"></param>
	/// <returns>Status as enum</returns>
	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
		return value is string antCount && double.TryParse(antCount, out var result) ? result : MainModel.MinEvaporationRate;
	}
	#endregion
}
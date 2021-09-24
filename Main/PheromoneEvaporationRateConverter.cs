using System;
using System.Globalization;
using System.Windows.Data;

using AntColony.Colony;

namespace AntColony.Main;
/// <summary>
/// Makes evaporation rate observable
/// </summary>
internal class PheromoneEvaporationRateConverter : IValueConverter {
	#region Converter methods
	/// <summary>
	/// Convert ant count to string
	/// </summary>
	/// <param name="value"></param>
	/// <param name="targetType"></param>
	/// <param name="parameter"></param>
	/// <param name="culture"></param>
	/// <returns>Status as string</returns>
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is double antCount ? antCount.ToString() : Pheromone.MinPheromoneEvaporationRate.ToString();

	/// <summary>
	/// Convert ant count to double
	/// </summary>
	/// <param name="value"></param>
	/// <param name="targetType"></param>
	/// <param name="parameter"></param>
	/// <param name="culture"></param>
	/// <returns>Status as enum</returns>
	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value is string antCount && double.TryParse(antCount, out var result) ? result : Pheromone.MinPheromoneEvaporationRate;
	#endregion
}
using System;
using System.Globalization;
using System.Windows.Data;

using AntColony.Colony;

namespace AntColony.Main;
internal class PheromoneEvaporationRateConverter : IValueConverter {
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is double antCount ? antCount.ToString() : PheromoneNode.MinPheromoneEvaporationRate.ToString();
	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value is string antCount && double.TryParse(antCount, out var result) ? result : PheromoneNode.MinPheromoneEvaporationRate;
}
using System;
using System.Globalization;
using System.Windows.Data;

using AntColony.Colony;

namespace AntColony.Main;
internal class AntCountConverter : IValueConverter {
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is int antCount ? antCount.ToString() : AntNode.MaxAntCount.ToString();
	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value is string antCount && int.TryParse(antCount, out var result) ? result : AntNode.MinAntCount;
}
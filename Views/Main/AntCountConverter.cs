using System;
using System.Globalization;
using System.Windows.Data;

using PGMLab.ViewModels.Main;

namespace PGMLab.Views.Main {
	/// <summary>
	/// Converts a state enum to string
	/// Based on: https://stackoverflow.com/questions/13354892/converting-from-rgb-ints-to-hex
	/// </summary>
	internal class AntCountConverter : IValueConverter {
		#region Converter methods
		/// <summary>
		/// Convert ant count to string
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns>State as string</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is int antCount ? antCount.ToString() : "0";
		/// <summary>
		/// Convert ant count to int
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns>State as enum</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value is string antCount && int.TryParse(antCount, out var result) ? result : 0;
		#endregion
	}
}
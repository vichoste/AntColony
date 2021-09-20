using System;
using System.Globalization;
using System.Windows.Data;

using PGMLab.ViewModels.Main;

namespace PGMLab.Views.Main {
	/// <summary>
	/// Converts a state enum to string
	/// Based on: https://stackoverflow.com/questions/13354892/converting-from-rgb-ints-to-hex
	/// </summary>
	internal class StatusConverter : IValueConverter {
		#region Converter methods
		/// <summary>
		/// Convert state enum to string
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns>State as string</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is State state ? state.ToString() : "Ready";
		/// <summary>
		/// Convert string to enum
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns>State as enum</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value is string state ? Enum.Parse(typeof(State), state) : State.Ready;
		#endregion
	}
}
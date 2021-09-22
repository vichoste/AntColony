using System;
using System.Globalization;
using System.Windows.Data;

namespace AntColony.Main {
	/// <summary>
	/// Makes the status observable
	/// </summary>
	public class StatusConverter : IValueConverter {
		#region Converter methods
		/// <summary>
		/// Convert state enum to string
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns>State as string</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is State state ? state.ToString() : State.Ready.ToString();
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
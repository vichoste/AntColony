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
		/// Convert status enum to string
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns>Status as string</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is Status status ? status.ToString() : Status.Ready.ToString();
		/// <summary>
		/// Convert string to enum
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns>Status as enum</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value is string status ? Enum.Parse(typeof(Status), status) : Status.Ready;
		#endregion
	}
}
using MaterialDesignThemes.Wpf;

namespace AntColony.Main {
	/// <summary>
	/// Model for the main window
	/// </summary>
	public class MainModel {
		#region Fields
		/// <summary>
		/// Sets the current maximize icon
		/// </summary>
		public PackIconKind? MaximizeIcon { get; set; }
		/// <summary>
		/// Current program status
		/// </summary>
		public Status Status { get; set; }
		/// <summary>
		/// Checks if the control button is pressed
		/// </summary>
		public bool IsControlPressed { get; set; }
		/// <summary>
		/// Current zoom value
		/// </summary>
		public double ZoomValue { get; set; }
		/// <summary>
		/// Checks if the program can execute the TSP
		/// </summary>
		public bool CanOperate { get; set; }
		/// <summary>
		/// Ant count for TSP
		/// </summary>
		public int AntCount { get; set; }
		/// <summary>
		/// Evaporation rate for TSP
		/// </summary>
		public double EvaporationRate { get; set; }
		#endregion
		#region Constants
		/// <summary>
		/// Minimum ant count
		/// </summary>
		public const int MinAntCount = 0;
		/// <summary>
		/// Maximum ant count
		/// </summary>
		public const int MaxAntCount = 10;
		/// <summary>
		/// Minimum evaporation rate
		/// </summary>
		public const double MinEvaporationRate = .001;
		/// <summary>
		/// Maximum evaporation rate
		/// </summary>
		public const double MaxEvaporationRate = 1;
		#endregion
	}
}

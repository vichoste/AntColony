using MaterialDesignThemes.Wpf;

namespace AntColony.Main;
/// <summary>
/// Model for the main window
/// </summary>
internal class MainModel {
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
	/// Border margin
	/// </summary>
	public int BorderMargin { get; set; }
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
	/// Zoom factor
	/// </summary>
	public const double ZoomFactor = 1;
	/// <summary>
	/// Minimum zoom factor
	/// </summary>
	public const double MinZoomFactor = 1;
	/// <summary>
	/// Maximum zoom factor
	/// </summary>
	public const double MaxZoomFactor = 24;
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
	/// <summary>
	/// Normal border margin
	/// </summary>
	public const int NormalBorderMargin = 8;
	/// <summary>
	/// Maximized border margin
	/// </summary>
	public const int MaximizedBorderMargin = 16;
	#endregion
}
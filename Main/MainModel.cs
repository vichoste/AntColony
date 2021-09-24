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
	/// Pheromone evaporation rate
	/// </summary>
	public double PheromoneEvaporationRate { get; set; }
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
	/// Normal border margin
	/// </summary>
	public const int NormalBorderMargin = 8;
	/// <summary>
	/// Maximized border margin
	/// </summary>
	public const int MaximizedBorderMargin = 16;
	#endregion
}
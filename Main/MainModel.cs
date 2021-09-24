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
	/// Checks if the control button is pressed
	/// </summary>
	public bool IsControlPressed { get; set; }
	/// <summary>
	/// Checks if the program can execute the TSP
	/// </summary>
	public bool CanOperate { get; set; }
	#endregion
	#region Constants
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
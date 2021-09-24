namespace AntColony.Colony;

/// <summary>
/// Model for graph
/// </summary>
internal class GraphModel {
	#region Fields
	/// <summary>
	/// Current pixels zoom
	/// </summary>
	public double PixelsZoom { get; set; }
	#endregion
	#region Constants
	/// <summary>
	/// Zoom factor
	/// </summary>
	public const double ZoomFactor = .5;
	/// <summary>
	/// Minimum zoom factor
	/// </summary>
	public const double MinZoomFactor = .5;
	/// <summary>
	/// Maximum zoom factor
	/// </summary>
	public const double MaxZoomFactor = 10;
	#endregion
}

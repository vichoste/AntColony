using System.Collections.Generic;
using System.Linq;

namespace AntColony.Colony;
/// <summary>
/// This is an ant
/// </summary>
internal class AntNode : Node {
	#region Attributes
	private readonly List<(int X, int Y)> _VisitedCoordinates;
	#endregion
	#region Fields
	/// <summary>
	/// Visited coordinates
	/// </summary>
	public List<(int X, int Y)> VisitedCoordinates => this._VisitedCoordinates.ToList();
	/// <summary>
	/// Original X coordinate of the ant
	/// </summary>
	public int OriginX { get; set; }
	/// <summary>
	/// Original Y coordinate of the ant
	/// </summary>
	public int OriginY { get; set; }
	#endregion
	#region Constructors
	/// <summary>
	/// Places an ant
	/// </summary>
	public AntNode() {
		this.ObservableNodeType = "Ant";
		this._VisitedCoordinates = new();
	}
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
	#endregion
	#region Methods
	/// <summary>
	/// Moves to the specified coordinate
	/// </summary>
	/// <param name="x">X coordinate</param>
	/// <param name="y">Y coordinate</param>
	public void MoveTo(int x, int y) {
		this.X = x;
		this.Y = y;
		this._VisitedCoordinates.Add((x, y));
	}
	#endregion
}
namespace AntColony.Colony;
/// <summary>
/// This is a node from the environment
/// </summary>
internal class Node {
	#region Fields
	/// <summary>
	/// Node ID
	/// </summary>
	public int Id { get; set; }
	/// <summary>
	/// X coordinate
	/// </summary>
	public int X { get; set; }
	/// <summary>
	/// Y coordinate
	/// </summary>
	public int Y { get; set; }
	/// <summary>
	/// X observable coordinate
	/// </summary>
	public int ObservableX => this.X * ObservableMargin;
	/// <summary>
	/// Y observable coordinate
	/// </summary>
	public int ObservableY => this.Y * ObservableMargin;
	/// <summary>
	/// IF this cell is marked as discovered
	/// </summary>
	public bool IsDiscovered { get; set; }
	#endregion
	#region Constants
	/// <summary>
	/// Maximum amount of nodes
	/// </summary>
	public const int MaxNodes = 300;
	/// <summary>
	/// Observable margin
	/// </summary>
	public const int ObservableMargin = 2;
	#endregion
}
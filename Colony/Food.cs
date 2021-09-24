namespace AntColony.Colony;
/// <summary>
/// This is an ant
/// </summary>
internal class Food : Node {
	#region Fields
	/// <summary>
	/// If this food cell is marked as discovered
	/// </summary>
	public bool IsDiscovered { get; set; }
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
}
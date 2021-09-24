namespace AntColony.Colony;
/// <summary>
/// This is an ant
/// </summary>
internal class AntNode : Node {
	#region Constructors
	/// <summary>
	/// Places an ant
	/// </summary>
	public AntNode() => this.ObservableNodeType = "Ant";
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
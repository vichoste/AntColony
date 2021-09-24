namespace AntColony.Colony;
/// <summary>
/// This is an ant
/// </summary>
internal class FoodNode : Node {
	#region Fields
	/// <summary>
	/// If this food cell is marked as discovered
	/// </summary>
	public bool IsDiscovered { get; set; }
	#endregion
	#region Constructors
	/// <summary>
	/// Places food
	/// </summary>
	public FoodNode() => this.ObservableNodeType = "Food";
	#endregion
}
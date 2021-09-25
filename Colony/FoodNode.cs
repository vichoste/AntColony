namespace AntColony.Colony;
/// <summary>
/// This is an ant
/// </summary>
internal class FoodNode : Node {
	#region Fields
	/// <summary>
	/// If this node is marked
	/// </summary>
	public bool IsVisited { get; set; }
	#endregion
	#region Constructors
	/// <summary>
	/// Places food
	/// </summary>
	public FoodNode() => this.ObservableNodeType = "Food";
	#endregion
}
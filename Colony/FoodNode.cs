namespace AntColony.Colony;
/// <summary>
/// This is an ant
/// </summary>
internal class FoodNode : Node {
	#region Constructors
	/// <summary>
	/// Places food
	/// </summary>
	public FoodNode() => this.ObservableNodeType = "Food";
	#endregion
}
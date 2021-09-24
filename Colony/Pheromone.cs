namespace AntColony.Colony;

internal class Pheromone : Node {
	#region Fields
	/// <summary>
	/// This value will evaporate over time
	/// </summary>
	public byte Strength { get; set; }
	#endregion
	#region Constants
	/// <summary>
	/// Minimum evaporation rate
	/// </summary>
	public const double MinPheromoneEvaporationRate = .001;
	/// <summary>
	/// Maximum evaporation rate
	/// </summary>
	public const double MaxPheromoneEvaporationRate = 1;
	#endregion
}

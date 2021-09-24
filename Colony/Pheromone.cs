namespace AntColony.Colony;

internal class Pheromone : Node {
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

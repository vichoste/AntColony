namespace AntColony.Colony;

internal class Pheromone : Node {
	#region Fields
	/// <summary>
	/// This value will evaporate over time
	/// </summary>
	public double Strength { get; private set; }
	/// <summary>
	/// Gets an RGB value for strength
	/// </summary>
	public byte ObservableStrength => (byte)(85000 * this.Strength / 333 - 85 / 333);
	#endregion
	#region Constructor
	/// <summary>
	/// Places a pheromone
	/// </summary>
	public Pheromone() => this.Strength = byte.MaxValue;
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
	#region Methods
	/// <summary>
	/// Evaporates the pheromone
	/// </summary>
	/// <param name="pheromoneEvaporationRate">Pheromone evaporation rate</param>
	/// <returns>New current strength</returns>
	public double Evaporate(double pheromoneEvaporationRate) {
		if (this.Strength - pheromoneEvaporationRate < 0) {
			this.Strength = 0;
		} else if (this.Strength > 0) {
			this.Strength -= pheromoneEvaporationRate;
		}
		return this.Strength;
	}
	/// <summary>
	/// Updates the feromone
	/// </summary>>
	public void Update() => this.Strength = 1;
	#endregion
}

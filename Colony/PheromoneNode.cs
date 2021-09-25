namespace AntColony.Colony;
internal class PheromoneNode : Node {
	public const double DefaultEvaporationRate = .75;
	public const double MinPheromoneEvaporationRate = .5;
	public const double MaxPheromoneEvaporationRate = .9;
	public double Strength { get; private set; }
	public PheromoneNode() => this.Strength = byte.MaxValue;
	public double Evaporate(double pheromoneEvaporationRate) {
		if (this.Strength - pheromoneEvaporationRate < 0) {
			this.Strength = 0;
		} else if (this.Strength > 0) {
			this.Strength -= pheromoneEvaporationRate;
		}
		return this.Strength;
	}
	public double Renew() => this.Strength = 1;
}

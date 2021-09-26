namespace AntColony.Colony;
internal class PheromoneNode : Node {
	public const double DefaultEvaporationRate = .01;
	public const double MinPheromoneEvaporationRate = .01;
	public const double MaxPheromoneEvaporationRate = 1;
	public byte ObservableStrength => (byte)(85000 * this.Strength / 333 - 85 / 333);
	public double Strength { get; private set; }
	public PheromoneNode() => this.Strength = 1;
	public void Evaporate(double pheromoneEvaporationRate) {
		if (this.Strength - pheromoneEvaporationRate < 0) {
			this.Strength = 0;
		} else if (this.Strength > 0) {
			this.Strength -= pheromoneEvaporationRate;
		}
	}
	public void Renew() => this.Strength = 1;
}

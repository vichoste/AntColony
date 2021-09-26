namespace AntColony.Colony;
internal class PheromoneNode : Node {
	public const double DefaultEvaporationRate = .01;
	public const double MinPheromoneEvaporationRate = .01;
	public const double MaxPheromoneEvaporationRate = .999;
	public byte ObservableStrength => (byte)(255 - (85000 * this.Strength / 333 - 85 / 333));
	public double Strength { get; set; }
	public static PheromoneNode Clone(PheromoneNode pheromone) {
		var clone = new PheromoneNode() {
			Id = pheromone.Id,
			X = pheromone.X,
			Y = pheromone.Y,
			Strength = pheromone.Strength
		};
		return clone;
	}
	public static PheromoneNode Evaporate(PheromoneNode pheromone, double pheromoneEvaporationRate) {
		if (pheromone.Strength - pheromoneEvaporationRate <= 0) {
			pheromone.Strength = 0;
		} else {
			pheromone.Strength -= pheromoneEvaporationRate;
		}
		return Clone(pheromone);
	}
	public static PheromoneNode Update(PheromoneNode pheromone) {
		pheromone.Strength = 1;
		return Clone(pheromone);
	}
}

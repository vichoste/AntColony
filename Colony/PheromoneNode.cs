namespace AntColony.Colony;
internal class PheromoneNode : Node {
	public const double DefaultEvaporationRate = .0075;
	public const double MinPheromoneEvaporationRate = .0075;
	public const double MaxPheromoneEvaporationRate = .999;
	public byte ObservableStrength => (byte)(255 - (85000 * this.Strength / 333 - 85 / 333));
	public double Strength { get; set; }
	public static PheromoneNode Clone(int id, int x, int y, double strength) {
		var clone = new PheromoneNode() {
			Id = id,
			X = x,
			Y = y,
			Strength = strength
		};
		return clone;
	}
	public static PheromoneNode Evaporate(PheromoneNode pheromone, double pheromoneEvaporationRate) => pheromone.Strength - pheromoneEvaporationRate <= 0
			? Clone(pheromone.Id, pheromone.X, pheromone.Y, 0)
			: Clone(pheromone.Id, pheromone.X, pheromone.Y, pheromone.Strength - pheromoneEvaporationRate);
	public static PheromoneNode Update(PheromoneNode pheromone) => Clone(pheromone.Id, pheromone.X, pheromone.Y, 1);
}

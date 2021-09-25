﻿namespace AntColony.Colony;
internal class PheromoneNode : Node {
	public const double MinPheromoneEvaporationRate = .001;
	public const double MaxPheromoneEvaporationRate = .999;
	public double Strength { get; private set; }
	public byte ObservableStrength => (byte)(85000 * this.Strength / 333 - 85 / 333);
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

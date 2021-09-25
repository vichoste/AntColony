namespace AntColony.Colony;
internal class ColonyModel {
	public const double MinZoomFactor = .1;
	public const double MaxZoomFactor = 10;
	public const double ZoomFactor = .1;
	public int AntCount { get; set; }
	public int MinCoordinate { get; set; }
	public int MaxCoordinate { get; set; }
	public double PheromoneEvaporationRate { get; set; }
}

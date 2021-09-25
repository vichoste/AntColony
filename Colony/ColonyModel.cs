using System.Collections.ObjectModel;
using System.Windows.Data;

namespace AntColony.Colony;
internal class ColonyModel {
	public ObservableCollection<AntNode>? AntNodes { get; set; }
	public int AntCount { get; set; }
	public ObservableCollection<FoodNode>? FoodNodes { get; set; }
	public int MinCoordinate { get; set; }
	public CompositeCollection? Nodes { get; set; }
	public int MaxCoordinate { get; set; }
	public double PheromoneEvaporationRate { get; set; }
	public ObservableCollection<PheromoneNode>? PheromoneNodes { get; set; }
}

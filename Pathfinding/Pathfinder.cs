using AntColony.Colony;

namespace AntColony.Algorithms;

/// <summary>
/// TSP pathfinder
/// </summary>
internal class Pathfinder {
	private GraphViewModel _GraphViewModel;
	/// <summary>
	/// Creates a pathfinder for TSP
	/// </summary>
	/// <param name="graphViewModel"></param>
	public Pathfinder(GraphViewModel graphViewModel) {
		this._GraphViewModel = graphViewModel;
	}
}

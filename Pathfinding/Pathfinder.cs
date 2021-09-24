using System;
using System.Collections.Generic;
using System.Windows.Documents;

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
	/// <summary>
	/// Starts the TSP
	/// </summary>
	/// <returns></returns>
	//public async Task Run() {
	//	var random = new Random();
	//	var ants = new List<AntNode>();
	//	random.Next(0, Node.MaxNodes);

	//}
}

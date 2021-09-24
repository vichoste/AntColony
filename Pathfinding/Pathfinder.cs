using System;

using AntColony.Colony;

namespace AntColony.Algorithms;

/// <summary>
/// TSP pathfinder
/// </summary>
internal class Pathfinder {
	private readonly GraphViewModel _GraphViewModel;
	/// <summary>
	/// Creates a pathfinder for TSP
	/// </summary>
	/// <param name="graphViewModel"></param>
	public Pathfinder(GraphViewModel graphViewModel) => this._GraphViewModel = graphViewModel;
	/// <summary>
	/// Starts the TSP
	/// </summary>
	/// <returns></returns>
	public async void Run() {
		var random = new Random();
		// Start colony in a random position
		var colonyStartX = random.Next(this._GraphViewModel.MinCoordinate, this._GraphViewModel.MaxCoordinate);
		var colonyStartY = random.Next(this._GraphViewModel.MinCoordinate, this._GraphViewModel.MaxCoordinate);
		for (var i = 0; i < this._GraphViewModel.AntCount; i++) {
			this._GraphViewModel.AddNode(new AntNode() {
				Id = i,
				X = colonyStartX,
				Y = colonyStartY
			});
		}
		this._GraphViewModel.OnPropertyChanged(nameof(this._GraphViewModel.Nodes));
	}
}

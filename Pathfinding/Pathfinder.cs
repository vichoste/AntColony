using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
	public async Task<Path> Run() {
		var random = new Random();
		var ants = new List<AntNode>();
		// Start colony in a random position
		var colonyStartX = random.Next(this._GraphViewModel.MinCoordinate, this._GraphViewModel.MaxCoordinate);
		var colonyStartY = random.Next(this._GraphViewModel.MinCoordinate, this._GraphViewModel.MaxCoordinate);
		for (var i = 0; i < this._GraphViewModel.AntCount; i++) {
			var ant = new AntNode() {
				Id = i,
				X = colonyStartX,
				Y = colonyStartY,
				OriginX = colonyStartX,
				OriginY = colonyStartY,
			};
			this._GraphViewModel.AddAnt(ant);
			ants.Add(ant);
		}
		this._GraphViewModel.OnPropertyChanged(nameof(this._GraphViewModel.AntNodes));
		// Start moving the ants
		return await this.GetBestPath(ants);
	}
	/// <summary>
	/// Move the ants
	/// </summary>
	/// <returns>The best possible path</returns>
	private async Task<Path> GetBestPath(List<AntNode> ants) {
		var antMoves = new List<Task<Path>>();
		for (var i = 0; i < this._GraphViewModel.AntCount; i++) {
			antMoves.Add(this.MoveAnt(ants[i]));
		}
		var result = await Task.WhenAny(antMoves);
		return null;
	}
	private async Task<Path> MoveAnt(AntNode ant) => null;
}
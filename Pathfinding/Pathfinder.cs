using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AntColony.Colony;

namespace AntColony.Algorithms;
// TODO this entire class
internal class Pathfinder {
	private readonly ColonyViewModel _ColonyViewModel;
	public Pathfinder(ColonyViewModel graphViewModel) => this._ColonyViewModel = graphViewModel;
	public async Task<Path> Run() {
		var random = new Random();
		var ants = new List<AntNode>();
		await Task.Run(() => {
			var colonyStartX = random.Next(this._ColonyViewModel.MinCoordinate, this._ColonyViewModel.MaxCoordinate);
			var colonyStartY = random.Next(this._ColonyViewModel.MinCoordinate, this._ColonyViewModel.MaxCoordinate);
			for (var i = 0; i < this._ColonyViewModel.AntCount; i++) {
				var ant = new AntNode() {
					Id = i,
					X = colonyStartX,
					Y = colonyStartY,
				};
				this._ColonyViewModel.AddAnt(ant);
				ants.Add(ant);
			}
		});
		this._ColonyViewModel.OnPropertyChanged(nameof(this._ColonyViewModel.AntNodes));
		return null;
	}
}
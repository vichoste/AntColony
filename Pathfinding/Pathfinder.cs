using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

using AntColony.Colony;

namespace AntColony.Algorithms;
internal class Pathfinder {
	private readonly ColonyViewModel _ColonyViewModel;
	private int _CurrentPheromoneId;
	public Pathfinder(ColonyViewModel colonyViewModel) => this._ColonyViewModel = colonyViewModel;
	private async Task CreateAnts() => await Task.Run(async () => {
		if (this._ColonyViewModel.AntNodes is not null) {
			var newAnts = new List<AntNode>();
			var colonyStartX = this._ColonyViewModel.MinCoordinate + RandomNumberGenerator.GetInt32(this._ColonyViewModel.MaxCoordinate - this._ColonyViewModel.MinCoordinate);
			var colonyStartY = this._ColonyViewModel.MinCoordinate + RandomNumberGenerator.GetInt32(this._ColonyViewModel.MaxCoordinate - this._ColonyViewModel.MinCoordinate);
			for (var i = 0; i < this._ColonyViewModel.AntCount; i++) {
				await Task.Run(() => {
					var newAnt = new AntNode() {
						Id = i,
						X = colonyStartX,
						Y = colonyStartY,
						Direction = Direction.North,
						ReturningHome = false,
					};
					newAnts.Add(newAnt);
				});
			}
			this._ColonyViewModel.AntNodes = new ObservableCollection<AntNode>(newAnts);
		}
	});
	private async Task CheckFood(AntNode ant) => await Task.Run(() => {
		if (this._ColonyViewModel.PheromoneNodes is not null && this._ColonyViewModel.FoodNodes is not null) {
			var food = this._ColonyViewModel.FoodNodes.ToList();
			var hitFood = food.Find(f => f.X == ant.X && f.Y == ant.Y);
			if (hitFood is not null) {
				ant.CanLayPheromones = true;
			}
		}
	});
	private async Task EvaporatePheromones(AntNode ant) => await Task.Run(async () => {
		if (this._ColonyViewModel.PheromoneNodes is not null && this._ColonyViewModel.AntNodes is not null) {
			var pheromones = this._ColonyViewModel.PheromoneNodes.ToList();
			var pheromonesInAnt = ant.Pheromones.ToList();
			var updatedPheromones = new List<PheromoneNode>();
			var updatedPheromonesInAnt = new List<PheromoneNode>();
			for (var i = 0; i < pheromones.Count; i++) {
				await Task.Run(() => {
					var updatedPheromone = PheromoneNode.Evaporate(pheromones[i], this._ColonyViewModel.PheromoneEvaporationRate);
					if (updatedPheromone.Strength > 0) {
						updatedPheromones.Add(updatedPheromone);
						var pheromoneInAnt = pheromonesInAnt.Find(p => p.Id == updatedPheromone.Id);
						if (pheromoneInAnt is not null) {
							updatedPheromonesInAnt.Add(updatedPheromone);
						}
					}
				});
			}
			if (updatedPheromonesInAnt.Count == 0) {
				ant.CanLayPheromones = false;
			}
			ant.Pheromones = updatedPheromones;
			this._ColonyViewModel.PheromoneNodes = new ObservableCollection<PheromoneNode>(updatedPheromones);
		}
	});
	private async Task LayPheromone(AntNode ant) => await Task.Run(() => {
		if (ant.CanLayPheromones && this._ColonyViewModel.PheromoneNodes is not null && ant.CanLayPheromones) {
			var pheromones = this._ColonyViewModel.PheromoneNodes.ToList();
			if (pheromones.Count > 0) {
				var maxStrength = pheromones.Max(p => p.Strength);
				var pheromone = new PheromoneNode() {
					Id = this._CurrentPheromoneId++,
					X = ant.X,
					Y = ant.Y,
					Strength = maxStrength
				};
				pheromones.Add(pheromone);
				ant.Pheromones.Add(pheromone);
			} else {
				var pheromone = new PheromoneNode() {
					Id = this._CurrentPheromoneId++,
					X = ant.X,
					Y = ant.Y,
					Strength = 1
				};
				pheromones.Add(pheromone);
				ant.Pheromones.Add(pheromone);
			}
			this._ColonyViewModel.PheromoneNodes = new ObservableCollection<PheromoneNode>(pheromones);
		}
	});
	private async Task MoveAnts() => await Task.Run(async () => {
		if (this._ColonyViewModel.AntNodes is not null && this._ColonyViewModel.FoodNodes is not null) {
			var ants = this._ColonyViewModel.AntNodes.ToList();
			var updatedAnts = new List<AntNode>();
			for (var i = 0; i < ants.Count; i++) {
				await Task.Run(async () => {
					var ant = ants[i];
					await this.EvaporatePheromones(ant);
					var movedAnt = await AntNode.MoveAnt(ant);
					await this.CheckFood(movedAnt);
					await this.LayPheromone(movedAnt);
					updatedAnts.Add(movedAnt);
				});
			}
			this._ColonyViewModel.AntNodes = new ObservableCollection<AntNode>(updatedAnts);
		}
	});
	public async Task Run() {
		await this.CreateAnts();
		while (true) {
			await this.MoveAnts();
		}
	}
}
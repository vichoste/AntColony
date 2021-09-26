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
	private async Task CheckFood() => await Task.Run(async () => {
		if (this._ColonyViewModel.AntNodes is not null && this._ColonyViewModel.FoodNodes is not null && this._ColonyViewModel.PheromoneNodes is not null) {
			var food = this._ColonyViewModel.FoodNodes.ToList();
			var ants = this._ColonyViewModel.AntNodes.ToList();
			var updatedAnts = new List<AntNode>();
			for (var i = 0; i < ants.Count; i++) {
				await Task.Run(() => {
					var hitFood = food.Find(f => f.X == ants[i].X && f.Y == ants[i].Y);
					if (hitFood is not null) {
						ants[i].CanLayPheromones = true;
					}
					updatedAnts.Add(ants[i]);
				});
			}
			this._ColonyViewModel.AntNodes = new ObservableCollection<AntNode>(updatedAnts);
		}
	});
	private async Task CheckPheromones() => await Task.Run(async () => {
		if (this._ColonyViewModel.AntNodes is not null && this._ColonyViewModel.PheromoneNodes is not null) {
			var ants = this._ColonyViewModel.AntNodes.ToList();
			var pheromones = this._ColonyViewModel.PheromoneNodes.ToList();
			var updatedPheromones = new List<PheromoneNode>();
			for (var i = 0; i < ants.Count; i++) {
				await Task.Run(() => {
					var ant = ants[i];
					if (ant.CanLayPheromones) {
						var foundPheromone = pheromones.Find(p => p.X == ant.X && p.Y == ant.Y);
						if (foundPheromone is not null) {
							var updatedPheromone = PheromoneNode.Update(foundPheromone);
							updatedPheromones.Add(updatedPheromone);
							ant.Pheromones.Add(updatedPheromone);
						} else {
							var newPheromone = new PheromoneNode() {
								Id = this._CurrentPheromoneId++,
								X = ant.X,
								Y = ant.Y,
								Strength = 1
							};
							updatedPheromones.Add(newPheromone);
							ant.Pheromones.Add(newPheromone);
						}
					} else {
						var foundPheromone = pheromones.Find(p => p.X == ant.X && p.Y == ant.Y);
						if (foundPheromone is not null) {
							var updatedPheromone = PheromoneNode.Update(foundPheromone);
							updatedPheromones.Add(updatedPheromone);
						}
					}
				});
			}
			this._ColonyViewModel.PheromoneNodes = new ObservableCollection<PheromoneNode>(updatedPheromones);
		}
	});
	public async Task MoveAnts() => await Task.Run(async () => {
		if (this._ColonyViewModel.AntNodes is not null) {
			var ants = this._ColonyViewModel.AntNodes.ToList();
			var updatedAnts = new List<AntNode>();
			for (var i = 0; i < ants.Count; i++) {
				await Task.Run(async () => {
					var ant = await AntNode.MoveAnt(ants[i]);
					updatedAnts.Add(ant);
				});
			}
			this._ColonyViewModel.AntNodes = new ObservableCollection<AntNode>(updatedAnts);
		}
	});
	public async Task Run() {
		await this.CreateAnts();
		while (true) {
			await this.MoveAnts();
			await this.CheckFood();
			await this.CheckPheromones();
		}
	}
}
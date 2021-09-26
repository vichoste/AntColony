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
						ReturningHome = false
					};
					newAnts.Add(newAnt);
				});
			}
			this._ColonyViewModel.AntNodes = new ObservableCollection<AntNode>(newAnts);
		}
	});
	private async Task CheckFoods() => await Task.Run(async () => {
		if (this._ColonyViewModel.FoodNodes is not null && this._ColonyViewModel.AntNodes is not null) {
			for (var i = 0; i < this._ColonyViewModel.AntNodes.Count; i++) {
				await Task.Run(() => {
					var currentAnt = this._ColonyViewModel.AntNodes[i];
					var hitFood = this._ColonyViewModel.FoodNodes.ToList().Find(f => f.X == currentAnt.X && f.Y == currentAnt.Y);
					if (hitFood is not null) {
						System.Diagnostics.Debug.WriteLine("Hit!");
					}
				});
			}
		}
	});
	private async Task CheckPheromone(AntNode ant) => await Task.Run(async () => {
		if (this._ColonyViewModel.AntNodes is not null && this._ColonyViewModel.PheromoneNodes is not null) {
			var pheromones = this._ColonyViewModel.PheromoneNodes.ToList();
			var updatedPheromones = new List<PheromoneNode>();
			for (var i = 0; i < pheromones.Count; i++) {
				await Task.Run(() => {
					if (ant.X == pheromones[i].X && ant.Y == pheromones[i].Y) {
						updatedPheromones.Add(PheromoneNode.Update(pheromones[i]));
					} else {
						updatedPheromones.Add(pheromones[i]);
					}
				});
			}
			this._ColonyViewModel.PheromoneNodes = new ObservableCollection<PheromoneNode>(updatedPheromones);
		}
	});
	private async Task EvaporatePheromones() => await Task.Run(async () => {
		if (this._ColonyViewModel.PheromoneNodes is not null) {
			var pheromones = this._ColonyViewModel.PheromoneNodes.ToList();
			var updatedPheromones = new List<PheromoneNode>();
			for (var i = 0; i < pheromones.Count; i++) {
				await Task.Run(() => {
					var updatedPheromone = PheromoneNode.Evaporate(pheromones[i], this._ColonyViewModel.PheromoneEvaporationRate);
					if (updatedPheromone.Strength > 0) {
						updatedPheromones.Add(updatedPheromone);
					}
				});
			}
			this._ColonyViewModel.PheromoneNodes = new ObservableCollection<PheromoneNode>(updatedPheromones);
		}
	});
	private async Task LayPheromone(int x, int y) => await Task.Run(() => {
		if (this._ColonyViewModel.PheromoneNodes is not null) {
			var pheromones = this._ColonyViewModel.PheromoneNodes.ToList();
			var newPheromone = new PheromoneNode() {
				Id = this._CurrentPheromoneId++,
				X = x,
				Y = y,
				Strength = 1
			};
			if (newPheromone.Id % 3 == 0) {
				pheromones.Add(newPheromone);
			}
			this._ColonyViewModel.PheromoneNodes = new ObservableCollection<PheromoneNode>(pheromones);
		}
	});
	private async Task MoveAnts() => await Task.Run(async () => {
		if (this._ColonyViewModel.AntNodes is not null && this._ColonyViewModel.FoodNodes is not null) {
			var currentAnts = this._ColonyViewModel.AntNodes.ToList();
			var updatedAnts = new List<AntNode>();
			for (var i = 0; i < currentAnts.Count; i++) {
				await Task.Run(async () => {
					var currentAnt = currentAnts[i];
					//await this.CheckPheromone(currentAnt);
					var movedAnt = await AntNode.MoveAnt(currentAnt);
					updatedAnts.Add(movedAnt);
					//await this.LayPheromone(currentAnt.X, currentAnt.Y);
				});
			}
			this._ColonyViewModel.AntNodes = new ObservableCollection<AntNode>(updatedAnts);
			await this.CheckFoods();
		}
	});
	public async Task Run() {
		await this.CreateAnts();
		while (true) {
			await this.MoveAnts();
			await this.EvaporatePheromones();
		}
	}
}
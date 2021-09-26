using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

using AntColony.Colony;
using AntColony.Pathfinding;

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
	private async Task CheckFoods() => await Task.Run(async () => {
		if (this._ColonyViewModel.FoodNodes is not null && this._ColonyViewModel.AntNodes is not null) {
			var ants = this._ColonyViewModel.AntNodes.ToList();
			var updatedAnts = new List<AntNode>();
			for (var i = 0; i < ants.Count; i++) {
				await Task.Run(() => {
					var ant = ants[i];
					var hitFood = this._ColonyViewModel.FoodNodes.ToList().Find(f => f.X == ant.X && f.Y == ant.Y);
					if (hitFood is not null) {
						ant.CanLayPheromones = true;
					}
					updatedAnts.Add(ant);
				});
			}
			this._ColonyViewModel.AntNodes = new ObservableCollection<AntNode>(updatedAnts);
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
		if (this._ColonyViewModel.PheromoneNodes is not null && this._ColonyViewModel.AntNodes is not null) {
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
	private static async Task<List<Probability>> GenerateProbabilities() {
		List<Probability> probabilities;
		probabilities = await Task.Run(() => probabilities = new() {
			new Probability() {
				Direction = Direction.North,
				Value = Probability.MinProbability + RandomNumberGenerator.GetInt32(Probability.MaxProbability - Probability.MinProbability)
			},
			new Probability() {
				Direction = Direction.South,
				Value = Probability.MinProbability + RandomNumberGenerator.GetInt32(Probability.MaxProbability - Probability.MinProbability)
			},
			new Probability() {
				Direction = Direction.East,
				Value = Probability.MinProbability + RandomNumberGenerator.GetInt32(Probability.MaxProbability - Probability.MinProbability)
			},
			new Probability() {
				Direction = Direction.West,
				Value = Probability.MinProbability + RandomNumberGenerator.GetInt32(Probability.MaxProbability - Probability.MinProbability)
			},
			new Probability() {
				Direction = Direction.NorthEast,
				Value = Probability.MinProbability + RandomNumberGenerator.GetInt32(Probability.MaxProbability - Probability.MinProbability)
			},
			new Probability() {
				Direction = Direction.NorthWest,
				Value = Probability.MinProbability + RandomNumberGenerator.GetInt32(Probability.MaxProbability - Probability.MinProbability)
			},
			new Probability() {
				Direction = Direction.SouthEast,
				Value = Probability.MinProbability + RandomNumberGenerator.GetInt32(Probability.MaxProbability - Probability.MinProbability)
			},
			new Probability() {
				Direction = Direction.SouthWest,
				Value = Probability.MinProbability + RandomNumberGenerator.GetInt32(Probability.MaxProbability - Probability.MinProbability)
			}
		});
		return probabilities;
	}
	private async Task LayPheromone(AntNode ant) => await Task.Run(() => {
		if (this._ColonyViewModel.PheromoneNodes is not null && ant.CanLayPheromones) {
			var pheromones = this._ColonyViewModel.PheromoneNodes.ToList();
			var newPheromone = new PheromoneNode() {
				Id = this._CurrentPheromoneId++,
				X = ant.X,
				Y = ant.Y,
				Strength = 1
			};
			pheromones.Add(newPheromone);
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
					await this.CheckPheromone(ant);
					var movedAnt = await AntNode.MoveAnt(ant);
					await this.LayPheromone(movedAnt);
					updatedAnts.Add(movedAnt);
				});
			}
			await this.CheckFoods();
			this._ColonyViewModel.AntNodes = new ObservableCollection<AntNode>(updatedAnts);
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
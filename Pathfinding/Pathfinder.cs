using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

using AntColony.Colony;
using AntColony.Pathfinding;

namespace AntColony.Algorithms;
// TODO this entire class
internal class Pathfinder {
	private readonly ColonyViewModel _ColonyViewModel;
	public Pathfinder(ColonyViewModel colonyViewModel) => this._ColonyViewModel = colonyViewModel;
	private async Task<List<AntNode>> CreateAnts() {
		var newAnts = new List<AntNode>();
		await Task.Run(() => {
			var colonyStartX = this._ColonyViewModel.MinCoordinate + RandomNumberGenerator.GetInt32(this._ColonyViewModel.MaxCoordinate - this._ColonyViewModel.MinCoordinate);
			var colonyStartY = this._ColonyViewModel.MinCoordinate + RandomNumberGenerator.GetInt32(this._ColonyViewModel.MaxCoordinate - this._ColonyViewModel.MinCoordinate);
			for (var i = 0; i < this._ColonyViewModel.AntCount; i++) {
				var ant = new AntNode() {
					Id = i,
					X = colonyStartX,
					Y = colonyStartY,
				};
				newAnts.Add(ant);
			}
		});
		return newAnts;
	}
	private async Task EvaporatePheromones() => await Task.Run(() => {
		if (this._ColonyViewModel.PheromoneNodes is not null) {
			var currentPheromones = this._ColonyViewModel.PheromoneNodes.ToList();
			var validPheromones = new List<PheromoneNode>();
			for (var j = 0; j < currentPheromones.Count; j++) {
				currentPheromones[j].Evaporate(this._ColonyViewModel.PheromoneEvaporationRate);
				if (currentPheromones[j].Strength > 0) {
					validPheromones.Add(currentPheromones[j]);
				}
			}
			this._ColonyViewModel.PheromoneNodes = new ObservableCollection<PheromoneNode>(validPheromones);
		}
	});
	private async Task MoveAnts(List<AntNode> newAnts) {
		while (true) {
			for (var i = 0; i < this._ColonyViewModel.AntCount; i++) {
				await this.IterateAnts(newAnts, i);
			}
			await this.EvaporatePheromones();
		}
	}
	public async void Run() {
		var newAnts = await this.CreateAnts();
		await this.MoveAnts(newAnts);
	}
	private async Task IterateAnts(List<AntNode> newAnts, int i) => await Task.Run(() => {
		List<Probability> firstProbabilities = new() {
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
		};
		var ant = AntNode.MoveAnt(newAnts[i], firstProbabilities);
		if (this._ColonyViewModel.FoodNodes is not null) {
			for (var j = 0; j < this._ColonyViewModel.FoodNodes.Count; j++) {
				var matchingFood = this._ColonyViewModel.FoodNodes.ToList().Find(f => f.X == ant.X && f.Y == ant.Y);
				if (matchingFood is not null) {
				}
			}
		}
		if (this._ColonyViewModel.AntNodes is not null && this._ColonyViewModel.PheromoneNodes is not null) {
			for (var j = 0; j < this._ColonyViewModel.AntNodes.Count; j++) {
				var currentPheromones = this._ColonyViewModel.PheromoneNodes.ToList();
				var matchingPheromone = currentPheromones.Find(p => p.X == ant.X && p.Y == ant.Y);
				if (matchingPheromone is null) {
					var newPheromone = new PheromoneNode() {
						X = ant.X,
						Y = ant.Y
					};
					currentPheromones.Add(newPheromone);
				} else {
					matchingPheromone.Renew();
				}
				this._ColonyViewModel.PheromoneNodes = new ObservableCollection<PheromoneNode>(currentPheromones);
			}
		}
		this._ColonyViewModel.AntNodes = new ObservableCollection<AntNode>(newAnts);
	});
}
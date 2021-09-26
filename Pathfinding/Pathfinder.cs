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
	private async Task CreateAnts() {
		if (this._ColonyViewModel.AntNodes is not null) {
			var colonyStartX = this._ColonyViewModel.MinCoordinate + RandomNumberGenerator.GetInt32(this._ColonyViewModel.MaxCoordinate - this._ColonyViewModel.MinCoordinate);
			var colonyStartY = this._ColonyViewModel.MinCoordinate + RandomNumberGenerator.GetInt32(this._ColonyViewModel.MaxCoordinate - this._ColonyViewModel.MinCoordinate);
			var newAnts = new List<AntNode>();
			for (var i = 0; i < this._ColonyViewModel.AntCount; i++) {
				await Task.Run(() => {
					var newAnt = new AntNode() {
						Id = i,
						X = colonyStartX,
						Y = colonyStartY,
					};
					newAnts.Add(newAnt);
				});
			}
			this._ColonyViewModel.AntNodes = new ObservableCollection<AntNode>(newAnts);
		}
	}
	private async Task CheckAntHit() {
		if (this._ColonyViewModel.FoodNodes is not null && this._ColonyViewModel.AntNodes is not null) {
			for (var i = 0; i < this._ColonyViewModel.AntNodes.Count; i++) {
				await Task.Run(() => {
					var currentAnt = this._ColonyViewModel.AntNodes[i];
					var matchingFood = this._ColonyViewModel.FoodNodes.ToList().Find(f => f.X == currentAnt.X && f.Y == currentAnt.Y);
					if (matchingFood is not null) {
						System.Diagnostics.Debug.WriteLine("Hit!");
					}
				});
			}
		}
	}
	private async Task EvaporatePheromones() => await Task.Run(() => {
		if (this._ColonyViewModel.PheromoneNodes is not null) {
			var currentPheromones = this._ColonyViewModel.PheromoneNodes.ToList();
			_ = currentPheromones.RemoveAll(p => p.Strength == 0);
			this._ColonyViewModel.PheromoneNodes = new ObservableCollection<PheromoneNode>(currentPheromones);
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

	private async Task MoveAnts() {
		if (this._ColonyViewModel.AntNodes is not null && this._ColonyViewModel.FoodNodes is not null) {
			var currentAnts = this._ColonyViewModel.AntNodes.ToList();
			var newAnts = new List<AntNode>();
			for (var i = 0; i < currentAnts.Count; i++) {
				await Task.Run(async () => {
					var currentAnt = currentAnts[i];
					var movedAnt = await this.MoveAnt(currentAnt);
					newAnts.Add(movedAnt);
				});
			}
			await this.CheckAntHit();
			this._ColonyViewModel.AntNodes = new ObservableCollection<AntNode>(newAnts);
			//if (this._ColonyViewModel.AntNodes is not null && this._ColonyViewModel.PheromoneNodes is not null) {
			//	for (var j = 0; j < this._ColonyViewModel.AntNodes.Count; j++) {
			//		await Task.Run(() => {
			//			var currentPheromones = this._ColonyViewModel.PheromoneNodes.ToList();
			//			var hitPheromones = currentPheromones.Where(p => p.X == movedAnt.X && p.Y == movedAnt.Y).ToList();
			//			if (hitPheromones.Count == 0) {
			//				var newPheromone = new PheromoneNode() {
			//					X = movedAnt.X,
			//					Y = movedAnt.Y
			//				};
			//				this._ColonyViewModel.AddPheromoneObservable(newPheromone);
			//			} else {
			//				for (var i = 0; i < hitPheromones.Count; i++) {
			//					hitPheromones[i].Renew();
			//				}
			//			}
			//		});
			//	}
			//}
			//await this.EvaporatePheromones();
		}
	}
	public async void Run() {
		await this.CreateAnts();
		while (true) {
			await this.MoveAnts();
		}
	}
	private async Task<AntNode> MoveAnt(AntNode currentAnt) {
		if (this._ColonyViewModel.AntNodes is not null && this._ColonyViewModel.FoodNodes is not null) {
			var movedAnt = AntNode.MoveAnt(currentAnt, await GenerateProbabilities());
			return movedAnt;
		}
		return currentAnt;
	}
}
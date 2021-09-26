using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using AntColony.Colony;
using AntColony.Pathfinding;

namespace AntColony.Algorithms;
// TODO this entire class
internal class Pathfinder {
	private readonly ColonyViewModel _ColonyViewModel;
	public Pathfinder(ColonyViewModel colonyViewModel) => this._ColonyViewModel = colonyViewModel;
	public async Task Run() {
		var newAnts = new List<AntNode>();
		await Task.Run(() => {
			var random = new Random((int)DateTime.Now.Ticks);
			var colonyStartX = random.Next(this._ColonyViewModel.MinCoordinate, this._ColonyViewModel.MaxCoordinate);
			var colonyStartY = random.Next(this._ColonyViewModel.MinCoordinate, this._ColonyViewModel.MaxCoordinate);
			for (var i = 0; i < this._ColonyViewModel.AntCount; i++) {
				var ant = new AntNode() {
					Id = i,
					X = colonyStartX,
					Y = colonyStartY,
				};
				this._ColonyViewModel.AddAnt(ant);
				newAnts.Add(ant);
			}
			if (this._ColonyViewModel.AntNodes is not null) {
				for (var it = 0; it < 10000; it++) {
					for (var i = 0; i < this._ColonyViewModel.AntCount; i++) {
						List<Probability> firstProbabilities = new() {
							new Probability() {
								Direction = Direction.North,
								Value = random.Next(Probability.MinProbability, Probability.MaxProbability)
							},
							new Probability() {
								Direction = Direction.South,
								Value = random.Next(Probability.MinProbability, Probability.MaxProbability)
							},
							new Probability() {
								Direction = Direction.East,
								Value = random.Next(Probability.MinProbability, Probability.MaxProbability)
							},
							new Probability() {
								Direction = Direction.West,
								Value = random.Next(Probability.MinProbability, Probability.MaxProbability)
							},
							new Probability() {
								Direction = Direction.NorthEast,
								Value = random.Next(Probability.MinProbability, Probability.MaxProbability)
							},
							new Probability() {
								Direction = Direction.NorthWest,
								Value = random.Next(Probability.MinProbability, Probability.MaxProbability)
							},
							new Probability() {
								Direction = Direction.SouthEast,
								Value = random.Next(Probability.MinProbability, Probability.MaxProbability)
							},
							new Probability() {
								Direction = Direction.SouthWest,
								Value = random.Next(Probability.MinProbability, Probability.MaxProbability)
							}
						};
						var ant = AntNode.MoveAnt(newAnts[i], firstProbabilities);
						if (this._ColonyViewModel.FoodNodes is not null) {
							for (var j = 0; j < this._ColonyViewModel.FoodNodes.Count; j++) {
								var matchingNode = this._ColonyViewModel.FoodNodes.ToList().Find(f => f.X == ant.X && f.Y == ant.Y);
								if (matchingNode is not null) {
									System.Diagnostics.Debug.WriteLine("Hit!");
								}
							}
						}
						var newPheromone = new PheromoneNode() {
							X = ant.X,
							Y = ant.Y
						};
					}
					this._ColonyViewModel.AntNodes = new ObservableCollection<AntNode>(newAnts);
				}
			}
		});
	}
}
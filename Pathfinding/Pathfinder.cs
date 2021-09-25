using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AntColony.Colony;
using AntColony.Pathfinding;

namespace AntColony.Algorithms;
// TODO this entire class
internal class Pathfinder {
	private readonly ColonyViewModel _ColonyViewModel;
	public Pathfinder(ColonyViewModel colonyViewModel) => this._ColonyViewModel = colonyViewModel;
	public async Task<Path> Run() {
		var ants = await CreateAnts(this._ColonyViewModel);
		await MakeFirstAntMovement(this._ColonyViewModel, ants);
		return null;
	}
	private static async Task<List<AntNode>> CreateAnts(ColonyViewModel colonyViewModel) {
		var ants = new List<AntNode>();
		await Task.Run(() => {
			var random = new Random();
			var colonyStartX = random.Next(colonyViewModel.MinCoordinate, colonyViewModel.MaxCoordinate);
			var colonyStartY = random.Next(colonyViewModel.MinCoordinate, colonyViewModel.MaxCoordinate);
			for (var i = 0; i < colonyViewModel.AntCount; i++) {
				var ant = new AntNode() {
					Id = i,
					X = colonyStartX,
					Y = colonyStartY,
				};
				colonyViewModel.AddAnt(ant);
				ants.Add(ant);
			}
		});
		return ants;
	}
	private static async Task MakeFirstAntMovement(ColonyViewModel colonyViewModel, List<AntNode> ants) => await Task.Run(() => {
		colonyViewModel.FlushAnts();
		var random = new Random();
		for (var i = 0; i < ants.Count; i++) {
			var firstProbabilities = new List<Probability>() {
				new Probability() {
					Direction = Direction.North,
					Value = random.NextDouble()
				},
				new Probability() {
					Direction = Direction.South,
					Value = random.NextDouble()
				},
				new Probability() {
					Direction = Direction.East,
					Value = random.NextDouble()
				},
				new Probability() {
					Direction = Direction.West,
					Value = random.NextDouble()
				},
				new Probability() {
					Direction = Direction.NorthEast,
					Value = random.NextDouble()
				},
				new Probability() {
					Direction = Direction.NorthWest,
					Value = random.NextDouble()
				},
				new Probability() {
					Direction = Direction.SouthEast,
					Value = random.NextDouble()
				},
				new Probability() {
					Direction = Direction.SouthWest,
					Value = random.NextDouble()
				}
			};
			var ant = AntNode.MoveAnt(ants[i], firstProbabilities);
			colonyViewModel.AddAnt(ant);
		}
	});
}
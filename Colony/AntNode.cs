using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AntColony.Colony;
internal class AntNode : Node {
	public const int ChunkStep = 1;
	public const int Factor = 10000;
	public const int Step = 1;
	public const int DefaultAntCount = 2;
	public const int MinAntCount = 1;
	public const int MinStrength = 0;
	public const int MaxAntCount = 4;
	public bool CanLayPheromones { get; set; }
	public List<PheromoneNode> Pheromones { get; set; }
	public AntNode() => this.Pheromones = new List<PheromoneNode>();
	private static async Task<List<double>> GetStrongestDistance(AntNode ant, List<PheromoneNode> pheromones) => await Task.Run(() => {
		var strengths = new List<double>();
		var foundNorthPheromone = pheromones.Find(p => p.X - Step == ant.X && p.Y == ant.Y);
		var foundSouthPheromone = pheromones.Find(p => p.X + Step == ant.X && p.Y == ant.Y);
		var foundEastPheromone = pheromones.Find(p => p.X == ant.X && p.Y - Step == ant.Y);
		var foundWestPheromone = pheromones.Find(p => p.X == ant.X && p.Y + Step == ant.Y);
		strengths.Add(foundNorthPheromone is not null ? foundNorthPheromone.Strength * Factor : MinStrength);
		strengths.Add(foundSouthPheromone is not null ? foundSouthPheromone.Strength * Factor : MinStrength);
		strengths.Add(foundEastPheromone is not null ? foundEastPheromone.Strength * Factor : MinStrength);
		strengths.Add(foundWestPheromone is not null ? foundWestPheromone.Strength * Factor : MinStrength);
		return strengths;
	});
	public static AntNode Clone(bool canLayPheromones, int id, int x, int y, List<PheromoneNode> pheromones) {
		var clone = new AntNode() {
			CanLayPheromones = canLayPheromones,
			Id = id,
			X = x,
			Y = y,
			Pheromones = pheromones.ToList(),
		};
		return clone;
	}
	public static async Task<AntNode> MoveAnt(AntNode ant, List<PheromoneNode> pheromones) => await Task.Run(async () => {
		var strengths = await GetStrongestDistance(ant, pheromones);
		if (strengths.Sum() == 0) {
			var y = RandomNumberGenerator.GetInt32(-1, 2);
			var x = RandomNumberGenerator.GetInt32(-1, 2);
			return Clone(ant.CanLayPheromones, ant.Id, ant.X + x, ant.Y + y, ant.Pheromones);
		}
		return await Task.Run(() => {
			var x = 0;
			var y = 0;
			x += Math.Sign(strengths[0] * RandomNumberGenerator.GetInt32(0, 2)) + RandomNumberGenerator.GetInt32(0, 2);
			x -= Math.Sign(strengths[1] * RandomNumberGenerator.GetInt32(0, 2)) + RandomNumberGenerator.GetInt32(0, 2);
			y += Math.Sign(strengths[2] * RandomNumberGenerator.GetInt32(0, 2)) + RandomNumberGenerator.GetInt32(0, 2);
			y -= Math.Sign(strengths[3] * RandomNumberGenerator.GetInt32(0, 2)) + RandomNumberGenerator.GetInt32(0, 2);
			return Clone(ant.CanLayPheromones, ant.Id, ant.X + x, ant.Y + y, ant.Pheromones);
		});
	});
}
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace AntColony.Colony;
internal class AntNode : Node {
	public const int ChunkStep = 1;
	public const int Step = 1;
	public const int DefaultAntCount = 2;
	public const int MinAntCount = 1;
	public const int MaxAntCount = 4;
	public bool CanLayPheromones { get; set; }
	public int SurroundingMoves { get; set; }
	public bool ReturningHome { get; set; }
	public Direction Direction { get; set; }
	public List<PheromoneNode> Pheromones { get; set; }
	public AntNode() => this.Pheromones = new List<PheromoneNode>();
	public static AntNode Clone(AntNode ant) {
		var clone = new AntNode() {
			Id = ant.Id,
			X = ant.X,
			Y = ant.Y,
			Direction = ant.Direction,
			ReturningHome = ant.ReturningHome,
			SurroundingMoves = ant.SurroundingMoves,
			Pheromones = ant.Pheromones.ToList(),
			CanLayPheromones = ant.CanLayPheromones
		};
		return clone;
	}
	public static async Task<AntNode> MoveAnt(AntNode ant) => await Task.Run(async () => {
		System.Diagnostics.Debug.WriteLine(ant.CanLayPheromones);
		if (ant.SurroundingMoves == 8) {
			var choose = await Task.Run(() => RandomNumberGenerator.GetInt32(0, 4));
			var x = 0;
			var y = 0;
			switch (choose) {
				case 0:
					x = ant.X + ChunkStep;
					y = ant.Y;
					break;
				case 1:
					x = ant.X;
					y = ant.Y - ChunkStep;
					break;
				case 2:
					x = ant.X - ChunkStep;
					y = ant.Y;
					break;
				case 3:
					x = ant.X;
					y = ant.Y + ChunkStep;
					break;
			}
			var antInNewChunk = new AntNode() {
				Id = ant.Id,
				X = x,
				Y = y,
				Direction = Direction.North,
				ReturningHome = ant.ReturningHome,
				SurroundingMoves = ant.SurroundingMoves,
				Pheromones = ant.Pheromones.ToList(),
				CanLayPheromones = ant.CanLayPheromones
			};
			return Clone(antInNewChunk);
		}
		switch (ant.Direction) {
			case Direction.North:
				if (!ant.ReturningHome) {
					ant.Y += Step;
					ant.ReturningHome = true;
					break;
				}
				ant.Y -= Step;
				ant.ReturningHome = false;
				ant.Direction = Direction.NorthWest;
				break;
			case Direction.South:
				if (!ant.ReturningHome) {
					ant.Y -= Step;
					ant.ReturningHome = true;
					break;
				}
				ant.Y += Step;
				ant.ReturningHome = false;
				ant.Direction = Direction.SouthEast;
				break;
			case Direction.East:
				if (!ant.ReturningHome) {
					ant.X += Step;
					ant.ReturningHome = true;
					break;
				}
				ant.X -= Step;
				ant.ReturningHome = false;
				ant.Direction = Direction.NorthEast;
				break;
			case Direction.West:
				if (!ant.ReturningHome) {
					ant.X -= Step;
					ant.ReturningHome = true;
					break;
				}
				ant.X += Step;
				ant.ReturningHome = false;
				ant.Direction = Direction.SouthWest;
				break;
			case Direction.NorthEast:
				if (!ant.ReturningHome) {
					ant.Y += Step;
					ant.X += Step;
					ant.ReturningHome = true;
					break;
				}
				ant.Y -= Step;
				ant.X -= Step;
				ant.ReturningHome = false;
				ant.Direction = Direction.North;
				break;
			case Direction.SouthEast:
				if (!ant.ReturningHome) {
					ant.Y -= Step;
					ant.X += Step;
					ant.ReturningHome = true;
					break;
				}
				ant.Y += Step;
				ant.X -= Step;
				ant.ReturningHome = false;
				ant.Direction = Direction.East;
				break;
			case Direction.NorthWest:
				if (!ant.ReturningHome) {
					ant.Y += Step;
					ant.X -= Step;
					ant.ReturningHome = true;
					break;
				}
				ant.Y -= Step;
				ant.X += Step;
				ant.ReturningHome = false;
				ant.Direction = Direction.West;
				break;
			case Direction.SouthWest:
				if (!ant.ReturningHome) {
					ant.Y -= Step;
					ant.X -= Step;
					ant.ReturningHome = true;
					break;
				}
				ant.Y += Step;
				ant.X += Step;
				ant.ReturningHome = false;
				ant.Direction = Direction.South;
				break;
		}
		ant.SurroundingMoves++;
		return Clone(ant);
	});
}
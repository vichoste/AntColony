using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AntColony.Colony;
internal class AntNode : Node {
	public const int ChunkStep = 1;
	public const int Step = 1;
	public const int DefaultAntCount = 2;
	public const int MinAntCount = 1;
	public const int MaxAntCount = 4;
	public bool CanLayPheromones { get; set; }
	public bool ReturningHome { get; set; }
	public int SurroundingMoves { get; set; }
	public Direction Direction { get; set; }
	public List<PheromoneNode> Pheromones { get; set; }
	public AntNode() => this.Pheromones = new List<PheromoneNode>();
	public static AntNode Clone(bool canLayPheromones, int id, int x, int y, Direction direction, List<PheromoneNode> pheromones, bool returningHome, int surroundingMoves) {
		var clone = new AntNode() {
			CanLayPheromones = canLayPheromones,
			Id = id,
			X = x,
			Y = y,
			Direction = direction,
			Pheromones = pheromones.ToList(),
			ReturningHome = returningHome,
			SurroundingMoves = surroundingMoves,
		};
		return clone;
	}
	public static async Task<AntNode> MoveAnt(AntNode ant) => await Task.Run(async () => {
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
			return Clone(ant.CanLayPheromones, ant.Id, x, y, Direction.North, ant.Pheromones.ToList(), false, ant.SurroundingMoves);
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
		return Clone(ant.CanLayPheromones, ant.Id, ant.X, ant.Y, Direction.North, ant.Pheromones.ToList(), ant.ReturningHome, ant.SurroundingMoves);
	});
}
using System.Collections.Generic;
using System.Linq;

using AntColony.Pathfinding;

namespace AntColony.Colony;
internal class AntNode : Node {
	public const int MinAntCount = 0;
	public const int MaxAntCount = 10;
	public int OriginX { get; set; }
	public int OriginY { get; set; }
	public void Move(List<Probability> probabilities) {
		var maxProbability = probabilities.MaxBy(p => p.Value);
		if (maxProbability is not null) {
			switch (maxProbability.Direction) {
				case Direction.North:
					this.Y = this.Y + 1 > MaxNodes ? this.Y - 1 : this.Y + 1;
					break;
				case Direction.South:
					this.Y = this.Y - 1 > MaxNodes ? this.Y + 1 : this.Y - 1;
					break;
				case Direction.East:
					this.X = this.X + 1 > MaxNodes ? this.X - 1 : this.X + 1;
					break;
				case Direction.West:
					this.X = this.X - 1 > MaxNodes ? this.X + 1 : this.X - 1;
					break;
				case Direction.NorthEast:
					this.Y = this.Y + 1 > MaxNodes ? this.Y - 1 : this.Y + 1;
					this.X = this.X + 1 > MaxNodes ? this.X - 1 : this.X + 1;
					break;
				case Direction.NorthWest:
					this.Y = this.Y + 1 > MaxNodes ? this.Y - 1 : this.Y + 1;
					this.X = this.X - 1 > MaxNodes ? this.X + 1 : this.X - 1;
					break;
				case Direction.SouthEast:
					this.Y = this.Y - 1 > MaxNodes ? this.Y + 1 : this.Y - 1;
					this.X = this.X + 1 > MaxNodes ? this.X - 1 : this.X + 1;
					break;
				case Direction.SouthWest:
					this.Y = this.Y - 1 > MaxNodes ? this.Y + 1 : this.Y - 1;
					this.X = this.X - 1 > MaxNodes ? this.X + 1 : this.X - 1;
					break;
			}
		}
	}
}
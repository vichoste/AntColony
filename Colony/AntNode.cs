using System.Collections.Generic;
using System.Linq;

using AntColony.Pathfinding;

namespace AntColony.Colony;
internal class AntNode : Node {
	public const int MinAntCount = 0;
	public const int MaxAntCount = 10;
	public static AntNode Clone(AntNode ant) {
		var clone = new AntNode() {
			Id = ant.Id,
			X = ant.X,
			Y = ant.Y
		};
		return clone;
	}
	public static AntNode MoveAnt(AntNode ant, List<Probability> probabilities) {
		var maxProbability = probabilities.MaxBy(p => p.Value);
		if (maxProbability is not null) {
			switch (maxProbability.Direction) {
				case Direction.North:
					ant.Y = ant.Y + 1 > Node.MaxNodes ? ant.Y - 1 : ant.Y + 1;
					break;
				case Direction.South:
					ant.Y = ant.Y - 1 > Node.MaxNodes ? ant.Y + 1 : ant.Y - 1;
					break;
				case Direction.East:
					ant.X = ant.X + 1 > Node.MaxNodes ? ant.X - 1 : ant.X + 1;
					break;
				case Direction.West:
					ant.X = ant.X - 1 > Node.MaxNodes ? ant.X + 1 : ant.X - 1;
					break;
				case Direction.NorthEast:
					ant.Y = ant.Y + 1 > Node.MaxNodes ? ant.Y - 1 : ant.Y + 1;
					ant.X = ant.X + 1 > Node.MaxNodes ? ant.X - 1 : ant.X + 1;
					break;
				case Direction.NorthWest:
					ant.Y = ant.Y + 1 > Node.MaxNodes ? ant.Y - 1 : ant.Y + 1;
					ant.X = ant.X - 1 > Node.MaxNodes ? ant.X + 1 : ant.X - 1;
					break;
				case Direction.SouthEast:
					ant.Y = ant.Y - 1 > Node.MaxNodes ? ant.Y + 1 : ant.Y - 1;
					ant.X = ant.X + 1 > Node.MaxNodes ? ant.X - 1 : ant.X + 1;
					break;
				case Direction.SouthWest:
					ant.Y = ant.Y - 1 > Node.MaxNodes ? ant.Y + 1 : ant.Y - 1;
					ant.X = ant.X - 1 > Node.MaxNodes ? ant.X + 1 : ant.X - 1;
					break;
			}
			return AntNode.Clone(ant);
		}
		return ant;
	}
}
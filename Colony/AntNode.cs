using System.Collections.Generic;
using System.Linq;

namespace AntColony.Colony;
internal class AntNode : Node {
	private readonly List<(int X, int Y)> _VisitedCoordinates;
	public const int MinAntCount = 0;
	public const int MaxAntCount = 10;
	public int OriginX { get; set; }
	public int OriginY { get; set; }
	public List<(int X, int Y)> VisitedCoordinates => this._VisitedCoordinates.ToList();
	public AntNode() => this._VisitedCoordinates = new();
	public void Move(int x, int y) {
		this.X = x;
		this.Y = y;
		this._VisitedCoordinates.Add((x, y));
	}
}
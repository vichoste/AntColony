using System.Collections.Generic;
using System.ComponentModel;

namespace AntColony.Colony;

/// <summary>
/// Environment
/// </summary>
internal class GraphViewModel : INotifyPropertyChanged {
	#region Attributes
	private readonly List<Node> _Nodes;
	private readonly GraphModel _GraphModel;
	#endregion
	#region Fields
	/// <summary>
	/// Gets the current food nodes
	/// </summary>
	public List<Node> Nodes => new(this._Nodes);
	/// <summary>
	/// Current pixels zoom
	/// </summary>
	public double PixelsZoom {
		get => this._GraphModel.PixelsZoom;
		set {
			if (this._GraphModel.PixelsZoom != value) {
				this._GraphModel.PixelsZoom = value;
			}
		}
	}
	/// <summary>
	/// Ant count for TSP
	/// </summary>
	public int AntCount {
		get => this._GraphModel.AntCount;
		set {
			if (this._GraphModel.AntCount != value) {
				this._GraphModel.AntCount = value is >= AntNode.MinAntCount and <= AntNode.MaxAntCount ?
					value : value is < AntNode.MinAntCount ?
						AntNode.MinAntCount : AntNode.MaxAntCount;
				this.OnPropertyChanged(nameof(this.AntCount));
			}
		}
	}
	/// <summary>
	/// Evaporation rate for TSP
	/// </summary>
	public double PheromoneEvaporationRate {
		get => this._GraphModel.PheromoneEvaporationRate;
		set {
			if (this._GraphModel.PheromoneEvaporationRate != value) {
				this._GraphModel.PheromoneEvaporationRate = value is >= PheromoneNode.MinPheromoneEvaporationRate and <= PheromoneNode.MaxPheromoneEvaporationRate ?
					value : value is < PheromoneNode.MinPheromoneEvaporationRate ?
					PheromoneNode.MinPheromoneEvaporationRate : PheromoneNode.MaxPheromoneEvaporationRate;
				this.OnPropertyChanged(nameof(this.PheromoneEvaporationRate));
			}
		}
	}
	/// <summary>
	/// Minimum graph coordinate
	/// </summary>
	public int MinCoordinate {
		get => this._GraphModel.MinCoordinate;
		set {
			if (this._GraphModel.MinCoordinate != value && value < this.MinCoordinate) {
				this._GraphModel.MinCoordinate = value;
			}
		}
	}
	/// <summary>
	/// Minimum graph coordinate
	/// </summary>
	public int MaxCoordinate {
		get => this._GraphModel.MaxCoordinate;
		set {
			if (this._GraphModel.MaxCoordinate != value && value > this.MaxCoordinate) {
				this._GraphModel.MaxCoordinate = value;
			}
		}
	}
	#endregion
	#region Constructors
	/// <summary>
	/// Creates the environment
	/// </summary>
	public GraphViewModel() {
		this._Nodes = new();
		this._GraphModel = new GraphModel() {
			PixelsZoom = 1,
			MinCoordinate = int.MaxValue,
			MaxCoordinate = 0,
		};
	}
	#endregion
	#region Indexers
	/// <summary>
	/// Gets or sets a node
	/// </summary>
	/// <param name="index">Node index</param>
	/// <returns>Food</returns>
	public Node? this[int index] {
		get {
			this.OnPropertyChanged(nameof(this.Nodes));
			return this._Nodes is not null ? this._Nodes[index] : null;
		}
		set {
			if (value is not null) {
				_ = this._Nodes[index] = value;
				this.OnPropertyChanged(nameof(this.Nodes));
			}
		}
	}
	#endregion
	#region Events
	/// <summary>
	/// Event
	/// </summary>
	public event PropertyChangedEventHandler? PropertyChanged;
	/// <summary>
	/// When property changes, call this function
	/// </summary>
	/// <param name="value">Property name</param>
	public void OnPropertyChanged(string value) => this.PropertyChanged?.Invoke(this, new(value));
	#endregion
	#region Methods
	/// <summary>
	/// Adds a food node into the graph
	/// </summary>
	/// <param name="node">Food to add</param>
	public void AddNode(Node node) => this._Nodes.Add(node);
	#endregion
}

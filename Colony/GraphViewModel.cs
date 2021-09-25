using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

using AntColony.Main;

namespace AntColony.Colony;

/// <summary>
/// Environment
/// </summary>
internal class GraphViewModel : INotifyPropertyChanged {
	#region Attributes
	private readonly List<FoodNode> _FoodNodes;
	private readonly List<PheromoneNode> _PheromoneNodes;
	private readonly List<AntNode> _AntNodes;
	private readonly GraphModel _GraphModel;
	private readonly CompositeCollection _Nodes;
	#endregion
	#region Fields
	/// <summary>
	/// Gets the current food nodes
	/// </summary>
	public List<Node> FoodNodes => new(this._FoodNodes);
	/// <summary>
	/// Gets the current pheromone nodes
	/// </summary>
	public List<Node> PheromoneNodes => new(this._PheromoneNodes);
	/// <summary>
	/// Gets the current ant nodes
	/// </summary>
	public List<Node> AntNodes => new(this._AntNodes);
	/// <summary>
	/// All nodes
	/// </summary>
	public CompositeCollection Nodes => this._Nodes;
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
		this._AntNodes = new();
		this._FoodNodes = new();
		this._PheromoneNodes = new();
		this._Nodes = new() {
			this._AntNodes,
			this._FoodNodes,
			this._PheromoneNodes
		};
		this._GraphModel = new GraphModel() {
			PixelsZoom = 1,
			MinCoordinate = int.MaxValue,
			MaxCoordinate = 0,
		};
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
	/// <param name="node">Node to add</param>
	public void AddFood(FoodNode node) => this._FoodNodes.Add(node);
	/// <summary>
	/// Adds a pheromone node into the graph
	/// </summary>
	/// <param name="node">Node to add</param>
	public void AddPheromone(PheromoneNode node) => this._PheromoneNodes.Add(node);
	/// <summary>
	/// Adds an ant node into the graph
	/// </summary>
	/// <param name="node">Node to add</param>
	public void AddAnt(AntNode node) => this._AntNodes.Add(node);
	#endregion
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public ObservableCollection<Node> Nodes => new(this._Nodes);
	/// <summary>
	/// Current pixels zoom
	/// </summary>
	public double PixelsZoom {
		get => this._GraphModel.PixelsZoom;
		set {
			if (this._GraphModel.PixelsZoom != value) {
				this._GraphModel.PixelsZoom = value;
				this.OnPropertyChanged(nameof(this.PixelsZoom));
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
			PixelsZoom = GraphModel.MinZoomFactor
		};
		this.OnPropertyChanged(nameof(this.Nodes));
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
	/// <param name="food">Food to add</param>
	public void AddFood(FoodNode food) {
		this._Nodes.Add(food);
		this.OnPropertyChanged(nameof(this.Nodes));
	}
	#endregion
}

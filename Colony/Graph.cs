using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace AntColony.Colony;

/// <summary>
/// Environment
/// </summary>
internal class Graph : INotifyPropertyChanged {
	#region Attributes
	private readonly List<Node> _Nodes;
	#endregion
	#region Fields
	/// <summary>
	/// Gets the current nodes
	/// </summary>
	public ObservableCollection<Node> Nodes => new(this._Nodes);
	#endregion
	#region Constructors
	/// <summary>
	/// Creates the environment
	/// </summary>
	public Graph() {
		this._Nodes = new();
		this.OnPropertyChanged(nameof(this.Nodes));
	}
	#endregion
	#region Indexers
	/// <summary>
	/// Gets or marks a node as discovered
	/// </summary>
	/// <param name="node">Node name</param>
	/// <returns>Node</returns>
	public bool this[Node? node] {
		get {
			this.OnPropertyChanged(nameof(this.Nodes));
#pragma warning disable CS8602 // Dereference of a possibly null reference.
			return this._Nodes.ToList().Find(n => n.Id == node.Id).IsDiscovered;
		}
		set {
			this._Nodes.ToList().Find(n => n.Id == node.Id).IsDiscovered = value;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
			this.OnPropertyChanged(nameof(this.Nodes));
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
	/// Adds a node into the graph
	/// </summary>
	/// <param name="node">Node to add</param>
	public void AddNode(Node node) {
		this._Nodes.Add(node);
		this.OnPropertyChanged(nameof(this.Nodes));
	}
	#endregion
}

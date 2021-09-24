using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Navigation;

using AntColony.Main;

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
	public List<Node> Nodes => this._Nodes.ToList();
	#endregion
	#region Constructors
	/// <summary>
	/// Creates the environment
	/// </summary>
	private Graph(Node[,] cells) {
		this._Nodes = cells;
		this.OnPropertyChanged(nameof(this.Nodes));
	}
	#endregion
	#region Indexers
	/// <summary>
	/// Gets or marks a node as discovered
	/// </summary>
	/// <param name="node">Node name</param>
	/// <returns>Node</returns>
	public bool this[int node] {
		get {
			this.OnPropertyChanged(nameof(this.Nodes));
			return this._Nodes.Find(node => node.Id is node);
		}
		set {
			this._Nodes.Find(node => node.Id is node).IsDiscovered = value;
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
	public void OnPropertyChanged(string value) {
		this.PropertyChanged?.Invoke(this, new(value));
	}
	#endregion
	#region Asynchronous Singleton
	/// <summary>
	/// Creates a cell view model in an asynchronous way
	/// https://stackoverflow.com/questions/8145479/can-constructors-be-async
	/// </summary>
	/// <returns>New instance of the cell view model</returns>
	public static async Task<Graph> BuildCellViewModelAsync() {
		var cells = new Node[Node.MaxNodes, Node.MaxNodes];
		var tasks = new List<Task>();
		for (var i = 0; i < Node.MaxNodes; i++) {
			for (var j = 0; j < Node.MaxNodes; j++) {
				tasks.Add(new Task(() => { cells[i, j] = new(); }));
			}
		}
		await Task.WhenAll(tasks);
		return new Graph(cells);
	}
	#endregion
	#region Methods
	public bool AddNode(Node node) {
		this._Nodes.Add(node);
	}
	#endregion
}

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
	private readonly List<Food> _Foods;
	#endregion
	#region Fields
	/// <summary>
	/// Gets the current food nodes
	/// </summary>
	public ObservableCollection<Food> Foods => new(this._Foods);
	#endregion
	#region Constructors
	/// <summary>
	/// Creates the environment
	/// </summary>
	public Graph() {
		this._Foods = new();
		this.OnPropertyChanged(nameof(this.Foods));
	}
	#endregion
	#region Indexers
	/// <summary>
	/// Gets or marks a food node as discovered
	/// </summary>
	/// <param name="node">Food name</param>
	/// <returns>Food</returns>
	public bool this[Food? node] {
		get {
			this.OnPropertyChanged(nameof(this.Foods));
#pragma warning disable CS8602 // Dereference of a possibly null reference.
			return this._Foods.ToList().Find(n => n.Id == node.Id).IsDiscovered;
		}
		set {
			this._Foods.ToList().Find(n => n.Id == node.Id).IsDiscovered = value;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
			this.OnPropertyChanged(nameof(this.Foods));
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
	public void AddFood(Food food) {
		this._Foods.Add(food);
		this.OnPropertyChanged(nameof(this.Foods));
	}
	#endregion
}

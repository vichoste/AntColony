using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace AntColony.Colony;
internal class ColonyViewModel : INotifyPropertyChanged {
	private readonly ColonyModel _ColonyModel;
	public event PropertyChangedEventHandler? PropertyChanged;
	public ObservableCollection<AntNode>? AntNodes {
		get => this._ColonyModel.AntNodes;
		set {
			if (this._ColonyModel.AntNodes != value) {
				this._ColonyModel.AntNodes = value;
				this.OnPropertyChanged(nameof(this.AntNodes));
			}
		}
	}
	public int AntCount {
		get => this._ColonyModel.AntCount;
		set {
			if (this._ColonyModel.AntCount != value) {
				this._ColonyModel.AntCount = value is >= AntNode.MinAntCount and <= AntNode.MaxAntCount ?
					value : value is < AntNode.MinAntCount ?
						AntNode.MinAntCount : AntNode.MaxAntCount;
				this.OnPropertyChanged(nameof(this.AntCount));
			}
		}
	}
	public ObservableCollection<FoodNode>? FoodNodes {
		get => this._ColonyModel.FoodNodes;
		set {
			if (this._ColonyModel.FoodNodes != value) {
				this._ColonyModel.FoodNodes = value;
				this.OnPropertyChanged(nameof(this.FoodNodes));
			}
		}
	}
	public int MaxCoordinate {
		get => this._ColonyModel.MaxCoordinate;
		set {
			if (this._ColonyModel.MaxCoordinate != value && value > this.MaxCoordinate) {
				this._ColonyModel.MaxCoordinate = value;
				this.OnPropertyChanged(nameof(this.MaxCoordinate));
			}
		}
	}
	public CompositeCollection? Nodes => this._ColonyModel.Nodes;
	public int MinCoordinate {
		get => this._ColonyModel.MinCoordinate;
		set {
			if (this._ColonyModel.MinCoordinate != value && value < this.MinCoordinate) {
				this._ColonyModel.MinCoordinate = value;
				this.OnPropertyChanged(nameof(this.MinCoordinate));
			}
		}
	}
	public double PheromoneEvaporationRate {
		get => this._ColonyModel.PheromoneEvaporationRate;
		set {
			if (this._ColonyModel.PheromoneEvaporationRate != value) {
				this._ColonyModel.PheromoneEvaporationRate = value is >= PheromoneNode.MinPheromoneEvaporationRate and <= PheromoneNode.MaxPheromoneEvaporationRate ?
					value : value is < PheromoneNode.MinPheromoneEvaporationRate ?
					PheromoneNode.MinPheromoneEvaporationRate : PheromoneNode.MaxPheromoneEvaporationRate;
				this.OnPropertyChanged(nameof(this.PheromoneEvaporationRate));
			}
		}
	}
	public ObservableCollection<PheromoneNode>? PheromoneNodes {
		get => this._ColonyModel.PheromoneNodes;
		set {
			if (this._ColonyModel.PheromoneNodes != value) {
				this._ColonyModel.PheromoneNodes = value;
				this.OnPropertyChanged(nameof(this.PheromoneNodes));
			}
		}
	}
	public ColonyViewModel() {
		this._ColonyModel = new ColonyModel() {
			MinCoordinate = int.MaxValue,
			MaxCoordinate = 0,
			AntNodes = new(),
			FoodNodes = new(),
			PheromoneNodes = new(),
			Nodes = new()
		};
		_ = this._ColonyModel.Nodes.Add(this._ColonyModel.AntNodes);
		_ = this._ColonyModel.Nodes.Add(this._ColonyModel.FoodNodes);
		_ = this._ColonyModel.Nodes.Add(this._ColonyModel.PheromoneNodes);
	}
	public void OnPropertyChanged(string value) => this.PropertyChanged?.Invoke(this, new(value));
}

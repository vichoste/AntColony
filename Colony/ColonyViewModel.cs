using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace AntColony.Colony;
internal delegate void HitTest();
internal class ColonyViewModel : INotifyPropertyChanged {
	private readonly List<AntNode> _AntNodes;
	private readonly List<FoodNode> _FoodNodes;
	private readonly List<PheromoneNode> _PheromoneNodes;
	public event PropertyChangedEventHandler? PropertyChanged;
	public CompositeCollection Nodes { get; }
	public List<AntNode> AntNodes => this._AntNodes.ToList();
	public List<FoodNode> FoodNodes => this._FoodNodes.ToList();
	public List<PheromoneNode> PheromoneNodes => this._PheromoneNodes.ToList();
	public int AntCount {
		get => this.ColonyModel.AntCount;
		set {
			if (this.ColonyModel.AntCount != value) {
				this.ColonyModel.AntCount = value is >= AntNode.MinAntCount and <= AntNode.MaxAntCount ?
					value : value is < AntNode.MinAntCount ?
						AntNode.MinAntCount : AntNode.MaxAntCount;
				this.OnPropertyChanged(nameof(this.AntCount));
			}
		}
	}
	public int MaxCoordinate {
		get => this.ColonyModel.MaxCoordinate;
		set {
			if (this.ColonyModel.MaxCoordinate != value && value > this.MaxCoordinate) {
				this.ColonyModel.MaxCoordinate = value;
				this.OnPropertyChanged(nameof(this.MaxCoordinate));
			}
		}
	}
	public int MinCoordinate {
		get => this.ColonyModel.MinCoordinate;
		set {
			if (this.ColonyModel.MinCoordinate != value && value < this.MinCoordinate) {
				this.ColonyModel.MinCoordinate = value;
				this.OnPropertyChanged(nameof(this.MinCoordinate));
			}
		}
	}
	public double PheromoneEvaporationRate {
		get => this.ColonyModel.PheromoneEvaporationRate;
		set {
			if (this.ColonyModel.PheromoneEvaporationRate != value) {
				this.ColonyModel.PheromoneEvaporationRate = value is >= PheromoneNode.MinPheromoneEvaporationRate and <= PheromoneNode.MaxPheromoneEvaporationRate ?
					value : value is < PheromoneNode.MinPheromoneEvaporationRate ?
					PheromoneNode.MinPheromoneEvaporationRate : PheromoneNode.MaxPheromoneEvaporationRate;
				this.OnPropertyChanged(nameof(this.PheromoneEvaporationRate));
			}
		}
	}
	public ColonyModel ColonyModel { get; }
	public ColonyViewModel() {
		this._AntNodes = new();
		this._FoodNodes = new();
		this._PheromoneNodes = new();
		this.Nodes = new() {
			this._AntNodes,
			this._FoodNodes,
			this._PheromoneNodes
		};
		this.ColonyModel = new ColonyModel() {
			MinCoordinate = int.MaxValue,
			MaxCoordinate = 0,
		};
	}
	public void AddAnt(AntNode ant) => this._AntNodes.Add(ant);
	public void AddFood(FoodNode food) => this._FoodNodes.Add(food);
	public void AddPheromone(PheromoneNode pheromone) => this._PheromoneNodes.Add(pheromone);
	public void OnPropertyChanged(string value) => this.PropertyChanged?.Invoke(this, new(value));
}

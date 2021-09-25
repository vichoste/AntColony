using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace AntColony.Colony;
internal class ColonyViewModel : INotifyPropertyChanged {
	public event PropertyChangedEventHandler? PropertyChanged;
	public CompositeCollection Nodes { get; }
	public ObservableCollection<AntNode> AntNodes { get; }
	public ObservableCollection<FoodNode> FoodNodes { get; }
	public ObservableCollection<PheromoneNode> PheromoneNodes { get; }
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
		this.AntNodes = new();
		this.FoodNodes = new();
		this.PheromoneNodes = new();
		this.Nodes = new() {
			this.AntNodes,
			this.FoodNodes,
			this.PheromoneNodes
		};
		this.ColonyModel = new ColonyModel() {
			MinCoordinate = int.MaxValue,
			MaxCoordinate = 0,
		};
	}
	public void AddAnt(AntNode ant) => AddToAntObservableCollection(this.AntNodes, ant);
	public void AddFood(FoodNode food) => AddToFoodObservableCollection(this.FoodNodes, food);
	public void AddPheromone(PheromoneNode pheromone) => AddToPheromoneObservableCollection(this.PheromoneNodes, pheromone);
	public void OnPropertyChanged(string value) => this.PropertyChanged?.Invoke(this, new(value));
	public static void AddToAntObservableCollection(ObservableCollection<AntNode> observableCollection, AntNode ant) {
		Action<AntNode> addMethod = observableCollection.Add;
		_ = Application.Current.Dispatcher.BeginInvoke(addMethod, ant);
	}
	public static void AddToFoodObservableCollection(ObservableCollection<FoodNode> observableCollection, FoodNode food) {
		Action<FoodNode> addMethod = observableCollection.Add;
		_ = Application.Current.Dispatcher.BeginInvoke(addMethod, food);
	}
	public static void AddToPheromoneObservableCollection(ObservableCollection<PheromoneNode> observableCollection, PheromoneNode pheromone) {
		Action<PheromoneNode> addMethod = observableCollection.Add;
		_ = Application.Current.Dispatcher.BeginInvoke(addMethod, pheromone);
	}
}

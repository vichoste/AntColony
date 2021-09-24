using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using AntColony.Colony;

using AsyncAwaitBestPractices.MVVM;

using MaterialDesignThemes.Wpf;

using Microsoft.Win32;

namespace AntColony.Main;
/// <summary>
/// View model for the main window
/// </summary>
internal class MainViewModel : INotifyPropertyChanged {
	#region Model
	private readonly MainModel _MainModel;
	#endregion
	#region References
	private Graph? _Graph;
	#endregion
	#region Fields
	/// <summary>
	/// Sets the current maximize icon
	/// </summary>
	public PackIconKind? MaximizeIcon {
		get => this._MainModel.MaximizeIcon;
		set {
			if (this._MainModel.MaximizeIcon != value) {
				this._MainModel.MaximizeIcon = value;
				this.OnPropertyChanged(nameof(this.MaximizeIcon));
			}
		}
	}
	/// <summary>
	/// Current program status
	/// </summary>
	public Status Status {
		get => this._MainModel.Status;
		set {
			if (this._MainModel.Status != value) {
				this._MainModel.Status = value;
				this.OnPropertyChanged(nameof(this.Status));
			}
		}
	}
	/// <summary>
	/// Border margin
	/// </summary>>
	public int BorderMargin {
		get => this._MainModel.BorderMargin;
		set {
			if (this._MainModel.BorderMargin != value) {
				this._MainModel.BorderMargin = value;
				this.OnPropertyChanged(nameof(this.BorderMargin));
			}
		}
	}
	/// <summary>
	/// Checks if the control button is pressed
	/// </summary>
	public bool IsControlPressed {
		get => this._MainModel.IsControlPressed;
		set {
			if (this._MainModel.IsControlPressed != value) {
				this._MainModel.IsControlPressed = value;
				this.OnPropertyChanged(nameof(this.IsControlPressed));
			}
		}
	}
	/// <summary>
	/// Checks if the program can execute the TSP
	/// </summary>
	public bool CanOperate {
		get => this._MainModel.CanOperate;
		set {
			if (this._MainModel.CanOperate != value) {
				this._MainModel.CanOperate = value;
				this.OnPropertyChanged(nameof(this.CanOperate));
			}
		}
	}
	/// <summary>
	/// Ant count for TSP
	/// </summary>
	public int AntCount {
		get => this._MainModel.AntCount;
		set {
			if (this._MainModel.AntCount != value) {
				this._MainModel.AntCount = value is >= Ant.MinAntCount and <= Ant.MaxAntCount ?
					value : value is < Ant.MinAntCount ?
						Ant.MinAntCount : Ant.MaxAntCount;
				this.OnPropertyChanged(nameof(this.AntCount));
			}
		}
	}
	/// <summary>
	/// Evaporation rate for TSP
	/// </summary>
	public double PheromoneEvaporationRate {
		get => this._MainModel.PheromoneEvaporationRate;
		set {
			if (this._MainModel.PheromoneEvaporationRate != value) {
				this._MainModel.PheromoneEvaporationRate = value is >= Pheromone.MinPheromoneEvaporationRate and <= Pheromone.MaxPheromoneEvaporationRate ?
					value : value is < Pheromone.MinPheromoneEvaporationRate ?
					Pheromone.MinPheromoneEvaporationRate : Pheromone.MaxPheromoneEvaporationRate;
				this.OnPropertyChanged(nameof(this.PheromoneEvaporationRate));
			}
		}
	}
	/// <summary>
	/// Cell view model
	/// </summary>>
	public Graph? Graph {
		get => this._Graph;
		set {
			if (value is not null && this._Graph != value) {
				this._Graph = value;
				this.OnPropertyChanged(nameof(this.Graph));
			}
		}
	}
	/// <summary>
	/// Current pixels zoom
	/// </summary>
	public double PixelsZoom {
		get => this._Graph is not null ? this._Graph.PixelsZoom : 0;
		set {
			if (this._Graph is not null && this._Graph.PixelsZoom != value) {
				this._Graph.PixelsZoom = value;
				this.OnPropertyChanged(nameof(this.PixelsZoom));
			}
		}
	}
	#endregion
	#region Constructors
	/// <summary>
	/// Creates the view model
	/// </summary>
	public MainViewModel() {
		this.OpenTspFileCommand = new(async () => {
			this.Status = Status.Opening;
			var openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() is true) {
				this.Graph = new Graph();
				// Allow only TSP files
				if (!openFileDialog.FileName.ToLower().EndsWith(".tsp")) {
					_ = MessageBox.Show("Can't open file. Only *.tsp files are allowed!");
					return;
				}
				// Read all lines
				var lines = await AsyncFileReader.ReadAllLinesAsync(openFileDialog.FileName, Encoding.UTF8);
				if (int.Parse(lines[3].Split(':')[1].Trim()) > 300) {
					_ = MessageBox.Show("Can't open file. For performance reasons, only graphs with < 300 nodes are allowed!");
					return;
				}
				if (!lines[4].EndsWith("EUC_2D")) { // Only allow Euclidian 2D, because I don't have time for other graph types
					_ = MessageBox.Show("Can't open file. Only EUC_2D graphs are allowed!");
					return;
				}
				if (!lines[5].Equals("NODE_COORD_SECTION")) {
					_ = MessageBox.Show("Can't open file. Expected \"NODE_COORD_SECTION : \" : at line 6!");
					return;
				}
				var result = true;
				for (var i = 6; i < lines.Length - 1; i++) {
					await Task.Run(() => {
						// Parse integers (https://stackoverflow.com/questions/4961675/select-parsed-int-if-string-was-parseable-to-int)
						var splitted = lines[i].Split(' ').Select(str => {
							var success = int.TryParse(str, out var value);
							return (value, success);
						}).Where(pair => pair.success).Select(pair => pair.value).ToList();
						if (splitted.Count is not 3) { // This implies there are not integers at all
							result = false;
							return;
						}
						this.Graph.AddFood(new() {
							Id = splitted[0],
							X = splitted[1],
							Y = splitted[2]
						});
					});
					if (!result) {
						break;
					}
				}
				if (!result) {
					_ = MessageBox.Show("Can't open file. Non-integers were found in the file!");
					this.CanOperate = false;
				} else {
					this.CanOperate = true;
				}
			} else {
				this.Graph = new Graph();
				this.CanOperate = false;
			}
			this.Status = Status.Ready;
		});
		this._MainModel = new() {
			MaximizeIcon = PackIconKind.WindowMaximize,
			Status = Status.Ready,
			BorderMargin = 8,
			AntCount = 4,
			PheromoneEvaporationRate = .5
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
	#region Command properties
	/// <summary>
	/// Test command
	/// </summary>
	public AsyncCommand OpenTspFileCommand { get; set; }
	#endregion
}
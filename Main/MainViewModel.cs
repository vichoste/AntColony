using System;
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
	private Graph? _CellViewModel;
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
				this._MainModel.AntCount = value >= MainModel.MinAntCount && value <= MainModel.MaxAntCount
					? value
					: value < 0 ? MainModel.MinAntCount : MainModel.MaxAntCount;
				this.OnPropertyChanged(nameof(this.AntCount));
			}
		}
	}
	/// <summary>
	/// Evaporation rate for TSP
	/// </summary>
	public double EvaporationRate {
		get => this._MainModel.EvaporationRate;
		set {
			if (this._MainModel.EvaporationRate != value) {
				this._MainModel.EvaporationRate = value >= MainModel.MinEvaporationRate && value <= MainModel.MaxEvaporationRate
					? value
					: value < 0 ? MainModel.MinEvaporationRate : MainModel.MaxEvaporationRate;
				this.OnPropertyChanged(nameof(this.EvaporationRate));
			}
		}
	}
	/// <summary>
	/// Cell view model
	/// </summary>>
	public Graph? Graph {
		get => this._CellViewModel;
		set {
			if (this._CellViewModel != value) {
				this._CellViewModel = value;
				this.OnPropertyChanged(nameof(this.Graph));
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
				if (!lines[4].EndsWith("EUC_2D")) { // Only allow Euclidian 2D, because I don't have time for other graph types
					_ = MessageBox.Show("Can't open file. Only EUC_2D graphs are allowed!");
					return;
				}
				if (!lines[5].Equals("NODE_COORD_SECTION")) {
					_ = MessageBox.Show("Can't open file. Expected \"NODE_COORD_SECTION : \" : at line 6!");
					return;
				}
				for (var i = 6; i < lines.Length - 1; i++) {
					try {
						await Task.Run(() => {
							// Parse integers (https://stackoverflow.com/questions/4961675/select-parsed-int-if-string-was-parseable-to-int)
							var splitted = lines[i].Split(' ').Select(str => {
								var success = double.TryParse(str, out var value);
								return (value, success);
							}).Where(pair => pair.success).Select(pair => pair.value).ToList();
							this.Graph.AddNode(new() {
								Id = int.Parse(splitted[0].ToString()),
								X = splitted[0],
								Y = splitted[1]
							});
						});
					} catch (Exception) {
						_ = MessageBox.Show("Can't open file. For simplicity, only integers!");
						return;
					}
				}
				this.CanOperate = true;
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
			EvaporationRate = .5
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
	public void OnPropertyChanged(string value) {
		this.PropertyChanged?.Invoke(this, new(value));
	}
	#endregion
	#region Command properties
	/// <summary>
	/// Test command
	/// </summary>
	public AsyncCommand OpenTspFileCommand { get; set; }
	#endregion
}